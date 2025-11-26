# How to Add a Super Admin to Your Deployed PostgreSQL Database

You have **3 options** to add a super admin to your Railway PostgreSQL database:

---

## Option 1: Temporary Code Injection (Easiest & Recommended)

This method adds the super admin automatically when the app starts.

### Steps:

1. **Open `Program.cs`** and add this line **just before** `app.Run();` (at the very end):

```csharp
// Add super admin if none exists
await SuperAdminSeeder.AddSuperAdminIfNeeded(app.Services);

app.Run();
```

2. **Deploy to Railway** (push to GitHub or use Railway CLI):
```bash
git add .
git commit -m "Add super admin seeder"
git push
```

3. **Check Railway logs** to confirm:
```
No SuperAdmin found. Creating default SuperAdmin...
✓ SuperAdmin created successfully!
  Username: admin
  Email: admin@yourdomain.com
  Password: @Pass123
```

4. **Remove the code** from `Program.cs` after confirming the super admin was created:
```csharp
// Delete this line:
// await SuperAdminSeeder.AddSuperAdminIfNeeded(app.Services);

app.Run();
```

5. **Deploy again** to clean up the code.

### Default Credentials:
- **Username**: `admin`
- **Password**: `@Pass123`
- **Email**: `admin@yourdomain.com`

⚠️ **IMPORTANT**: Change the password immediately after first login!

---

## Option 2: Using Railway CLI + EF Core Migration

This method creates a proper database migration.

### Steps:

1. **Install Railway CLI** (if not already installed):
```bash
npm i -g @railway/cli
```

2. **Login and link to your project**:
```bash
railway login
railway link
```

3. **Create a new migration**:
```bash
dotnet ef migrations add AddProductionSuperAdmin
```

4. **Edit the migration file** in `Migrations/` folder:

Find the `Up` method and add:
```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    var passwordHasher = new PasswordHashingService();
    var hashedPassword = passwordHasher.HashPassword("@Pass123");
    
    migrationBuilder.Sql($@"
        INSERT INTO ""Users"" (""Id"", ""Username"", ""Email"", ""FirstName"", ""LastName"", ""PasswordHash"", ""UserRole"")
        VALUES (
            '{Guid.NewGuid()}',
            'admin',
            'admin@yourdomain.com',
            'System',
            'Administrator',
            '{hashedPassword}',
            'SuperAdmin'
        )
        ON CONFLICT DO NOTHING;
    ");
}
```

5. **Apply the migration to Railway**:
```bash
railway run dotnet ef database update
```

---

## Option 3: Direct SQL via Railway Dashboard

This method uses raw SQL directly in the Railway PostgreSQL console.

### Steps:

1. **Go to Railway Dashboard** → Your Project → PostgreSQL service

2. **Click "Data" tab** or use the **Query** feature

3. **Run this SQL** (but first, you need to generate the password hash):

#### Generate Password Hash First:

Create a temporary C# console app or use your existing app:

```csharp
using BackendTechnicalAssetsManagement.src.Services;

var passwordHasher = new PasswordHashingService();
var hash = passwordHasher.HashPassword("@Pass123");
Console.WriteLine($"Password Hash: {hash}");
```

Run it:
```bash
dotnet run
```

Copy the output hash.

#### Then Run This SQL:

```sql
INSERT INTO "Users" (
    "Id", 
    "Username", 
    "Email", 
    "FirstName", 
    "LastName", 
    "PasswordHash", 
    "UserRole", 
    "MiddleName", 
    "PhoneNumber", 
    "Status"
)
VALUES (
    gen_random_uuid(),
    'admin',
    'admin@yourdomain.com',
    'System',
    'Administrator',
    'PASTE_YOUR_GENERATED_HASH_HERE',
    'SuperAdmin',
    NULL,
    NULL,
    NULL
);

-- Verify it was created
SELECT "Id", "Username", "Email", "FirstName", "LastName", "UserRole" 
FROM "Users" 
WHERE "Username" = 'admin';
```

---

## Option 4: Use Existing Seed Data

If you haven't run migrations yet on Railway, the seed data in `ModelBuilderExtensions.cs` will automatically create a super admin:

- **Username**: `superadmin`
- **Password**: `@Pass123`
- **Email**: `superadmin@example.com`

Just run:
```bash
railway run dotnet ef database update
```

---

## Verification

After adding the super admin, test the login:

### Using Swagger UI:
1. Go to `https://your-app.railway.app/swagger`
2. Navigate to `/api/Auth/login`
3. Try logging in with:
```json
{
  "username": "admin",
  "password": "@Pass123"
}
```

### Using curl:
```bash
curl -X POST https://your-app.railway.app/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"@Pass123"}'
```

You should receive a JWT token in the response.

---

## Security Recommendations

After creating the super admin:

1. ✅ **Change the default password immediately**
2. ✅ **Update the email to a real admin email**
3. ✅ **Enable 2FA if your app supports it**
4. ✅ **Remove any temporary seeding code from production**
5. ✅ **Never commit passwords or hashes to Git**
6. ✅ **Use strong passwords (min 12 characters, mixed case, numbers, symbols)**

---

## Troubleshooting

### "User already exists"
Check existing users:
```sql
SELECT "Username", "Email", "UserRole" FROM "Users" WHERE "UserRole" = 'SuperAdmin';
```

### "Cannot connect to database"
Verify your connection string in Railway environment variables:
```bash
railway variables
```

### "Password hash doesn't work"
Make sure you're using BCrypt with work factor 11 (default in `PasswordHashingService`).

---

## Need Help?

- Check Railway logs: `railway logs`
- Check database connection: `railway run dotnet ef database update --verbose`
- Verify environment: `railway variables`
