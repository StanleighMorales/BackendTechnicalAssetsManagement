# Database Seed Data Summary

## Database Reset Completed ✓

The database has been successfully dropped, recreated, and seeded with fresh data.

## Default Login Credentials

**Default Password for all users:** `@Pass123`

## Seeded Users

### Super Admin (1)
- **Username:** superadmin
- **Email:** superadmin@example.com
- **Role:** SuperAdmin

### Admins (3)
1. **Username:** msantos | **Email:** maria.santos@example.com
2. **Username:** jdelacruz | **Email:** juan.delacruz@example.com
3. **Username:** areyes | **Email:** ana.reyes@example.com

### Staff (3)
1. **Username:** cmendoza | **Email:** carlos.mendoza@example.com | **Position:** Lab Technician
2. **Username:** rgarcia | **Email:** rosa.garcia@example.com | **Position:** Equipment Manager
3. **Username:** mtorres | **Email:** miguel.torres@example.com | **Position:** IT Support

### Teachers (4)
1. **Username:** awilliams | **Email:** alice.williams@example.com | **Dept:** Information Technology
2. **Username:** rcruz | **Email:** roberto.cruz@example.com | **Dept:** Computer Science
3. **Username:** efernandez | **Email:** elena.fernandez@example.com | **Dept:** Information Technology
4. **Username:** dramos | **Email:** david.ramos@example.com | **Dept:** Multimedia Arts

### Students (6)
1. **Username:** jdoe | **Email:** john.doe@student.example.com | **ID:** 2023-0001 | **Course:** Computer Science
2. **Username:** jsmith | **Email:** jane.smith@student.example.com | **ID:** 2023-0002 | **Course:** Information Technology
3. **Username:** pjones | **Email:** peter.jones@student.example.com | **ID:** 2023-0003 | **Course:** Computer Science
4. **Username:** mlopez | **Email:** maria.lopez@student.example.com | **ID:** 2023-0004 | **Course:** Multimedia Arts
5. **Username:** crivera | **Email:** carlos.rivera@student.example.com | **ID:** 2023-0005 | **Course:** Information Technology
6. **Username:** sgonzales | **Email:** sofia.gonzales@student.example.com | **ID:** 2024-0001 | **Course:** Computer Science

## Seeded Items (8)

### Currently Borrowed (4)
1. **HDMI Cable 10ft** (SN-HDMI-001) - Borrowed by John Doe
2. **Wireless Microphone** (SN-MIC-002) - Borrowed by Peter Jones
3. **Wireless Mouse** (SN-MOUSE-004) - Borrowed by Alice Williams (Teacher)
4. **Portable Bluetooth Speaker** (SN-SPK-003) - Available (was returned)

### Available (4)
5. **Mechanical Keyboard** (SN-KB-005) - Available (was returned)
6. **Extension Wire 15ft** (SN-EXT-006) - Available
7. **HDMI Cable 6ft** (SN-HDMI-007) - Available
8. **USB Microphone** (SN-MIC-008) - Available

## Lent Items History (5 records)

1. **John Doe** borrowed HDMI Cable 10ft (5 days ago) - **Status: Borrowed**
2. **Jane Smith** borrowed Portable Bluetooth Speaker (10 days ago, returned 2 days ago) - **Status: Returned**
3. **Peter Jones** borrowed Wireless Microphone (1 day ago) - **Status: Borrowed**
4. **Alice Williams** (Teacher) borrowed Wireless Mouse (3 days ago) - **Status: Borrowed**
5. **Roberto Cruz** (Teacher) borrowed Mechanical Keyboard (30 days ago, returned 15 days ago) - **Status: Returned**

## Images

All items and student profiles include the mock image from `wwwroot/images/mockImage.png` (14,610 bytes).

- Item images: ✓ Loaded
- Item barcode images: ✓ Generated
- Student profile pictures: ✓ Loaded
- Student ID pictures: ✓ Loaded
- Lent item barcode images: ✓ Generated

## Migration Details

- **Migration Name:** InitialCreate
- **Created:** 2025-11-25
- **Database:** technical_db
- **Server:** (localdb)\MSSQLLocalDB

## Next Steps

You can now:
1. Run the application: `dotnet run` or `run.bat`
2. Login with any of the seeded users using password: `@Pass123`
3. Test the borrowing/returning functionality
4. View the seeded items and lent items history
