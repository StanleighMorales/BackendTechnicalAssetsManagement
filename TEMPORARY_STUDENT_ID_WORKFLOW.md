# Temporary Student ID Workflow

## Overview
This document explains how the system handles student imports when real student ID numbers are not available at import time.

## Problem Statement
When importing students from Excel:
- Excel file only contains student names (FirstName, LastName, MiddleName)
- No real student ID numbers are available yet
- Students need accounts created immediately with generated passwords
- Need to track generated passwords even before students provide their real ID numbers

## Solution: Temporary Student IDs

### How It Works

#### 1. Excel Import Process
When importing students via Excel:
```
Input: Excel file with columns: LastName, FirstName, MiddleName (optional)
```

The system automatically:
1. Generates a **username** (e.g., `john.doe`, `jane.smith`)
2. Generates a **random password** (e.g., `aB3$xY9@kL2m`)
3. Generates a **temporary student ID** (format: `TEMP-YYYY-XXXXX`)
   - Example: `TEMP-2025-00001`, `TEMP-2025-00002`, etc.
4. Stores all information in the database

#### 2. Temporary Student ID Format
```
TEMP-{YEAR}-{SEQUENCE}
```
- **TEMP**: Prefix indicating this is a temporary ID
- **YEAR**: Current year (e.g., 2025)
- **SEQUENCE**: 5-digit sequential number (00001, 00002, etc.)

**Examples:**
- `TEMP-2025-00001`
- `TEMP-2025-00002`
- `TEMP-2025-00123`

#### 3. Student Login & Profile Completion
After import, students receive:
- Username: `john.doe`
- Password: `aB3$xY9@kL2m` (generated)
- Temporary Student ID: `TEMP-2025-00001`

Students can:
1. **Login** using their username and generated password
2. **View their credentials** including the temporary student ID
3. **Update their profile** with:
   - Real student ID number (replaces temporary ID)
   - Real email address
   - Real phone number
   - Course, section, year
   - Address information
   - ID pictures

#### 4. Updating to Real Student ID
When a student updates their profile with a real student ID:
- The temporary ID (`TEMP-2025-00001`) is **replaced** with the real ID (e.g., `2024-12345`)
- The **generated password remains stored** in the `GeneratedPassword` field
- All other student information is preserved
- The student can now be looked up by their real student ID

## Database Schema

### Student Table Fields
```csharp
public class Student : User
{
    public string? StudentIdNumber { get; set; }  // Can be temporary or real
    public string? GeneratedPassword { get; set; } // Always preserved
    // ... other fields
}
```

## API Endpoints

### 1. Import Students
```http
POST /api/v1/users/students/import
Content-Type: multipart/form-data

file: students.xlsx
```

**Response:**
```json
{
  "totalProcessed": 3,
  "successCount": 3,
  "failureCount": 0,
  "registeredStudents": [
    {
      "fullName": "John Doe",
      "username": "john.doe",
      "generatedPassword": "aB3$xY9@kL2m",
      "temporaryStudentId": "TEMP-2025-00001"
    },
    {
      "fullName": "Jane Smith",
      "username": "jane.smith",
      "generatedPassword": "xY7#pQ4@nM9z",
      "temporaryStudentId": "TEMP-2025-00002"
    }
  ],
  "errors": []
}
```

### 2. Get Student by ID Number (Temporary or Real)
```http
GET /api/v1/users/students/by-id-number/TEMP-2025-00001
Authorization: Bearer {token}
```

**Response:**
```json
{
  "data": {
    "id": "123e4567-e89b-12d3-a456-426614174000",
    "firstName": "John",
    "lastName": "Doe",
    "username": "john.doe",
    "studentIdNumber": "TEMP-2025-00001",
    "generatedPassword": "aB3$xY9@kL2m",
    "email": "john.doe@temporary.com",
    "phoneNumber": "0000000000",
    // ... other fields
  }
}
```

### 3. Complete Student Registration (Update Profile)
```http
POST /api/v1/users/students/{userId}/complete-registration
Authorization: Bearer {token}
Content-Type: multipart/form-data

studentIdNumber: 2024-12345  // Real student ID
email: john.doe@university.edu
phoneNumber: 1234567890
course: Computer Science
section: A
year: 3rd Year
// ... other fields
```

## Workflow Diagram

```
┌─────────────────────────────────────────────────────────────┐
│ 1. EXCEL IMPORT                                             │
│    Input: Names only (FirstName, LastName, MiddleName)     │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 2. SYSTEM GENERATES                                         │
│    ✓ Username: john.doe                                     │
│    ✓ Password: aB3$xY9@kL2m                                 │
│    ✓ Temporary ID: TEMP-2025-00001                          │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 3. STUDENT RECEIVES CREDENTIALS                             │
│    Username: john.doe                                       │
│    Password: aB3$xY9@kL2m                                   │
│    Temp ID: TEMP-2025-00001                                 │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 4. STUDENT LOGS IN                                          │
│    Uses username + generated password                       │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 5. STUDENT UPDATES PROFILE                                  │
│    Provides:                                                │
│    • Real Student ID: 2024-12345                            │
│    • Real Email: john@university.edu                        │
│    • Phone, Course, Section, Year, etc.                     │
└────────────────────┬────────────────────────────────────────┘
                     │
                     ▼
┌─────────────────────────────────────────────────────────────┐
│ 6. SYSTEM UPDATES                                           │
│    StudentIdNumber: TEMP-2025-00001 → 2024-12345            │
│    GeneratedPassword: aB3$xY9@kL2m (PRESERVED)              │
│    Email: temporary → real                                  │
│    Phone: temporary → real                                  │
└─────────────────────────────────────────────────────────────┘
```

## Benefits

### 1. Immediate Account Creation
- Students can be imported and accounts created immediately
- No need to wait for real student IDs

### 2. Password Traceability
- Generated passwords are always stored
- Can be retrieved even after student updates their profile
- Useful for password recovery or verification

### 3. Unique Identification
- Temporary IDs are guaranteed unique
- Sequential numbering prevents collisions
- Year-based format helps with organization

### 4. Seamless Transition
- Students can use temporary ID initially
- Smooth transition to real ID when available
- No data loss during the update

## Frontend Integration

### Displaying Student Credentials
The frontend can display both temporary and real student IDs:

```typescript
// In ViewStudentCredentials component
<div>
  <label>Student ID Number</label>
  <p>{student.studentIdNumber}</p>
  {student.studentIdNumber?.startsWith('TEMP-') && (
    <span className="badge">Temporary - Update in Profile</span>
  )}
</div>

<div>
  <label>Generated Password</label>
  <input 
    type={showPassword ? "text" : "password"}
    value={student.generatedPassword || "N/A"}
    disabled
  />
  <button onClick={() => setShowPassword(!showPassword)}>
    {showPassword ? "Hide" : "Show"}
  </button>
</div>
```

### Import Results Display
After importing students, show the temporary IDs:

```typescript
{importResults.registeredStudents.map(student => (
  <tr key={student.username}>
    <td>{student.fullName}</td>
    <td>{student.username}</td>
    <td>{student.generatedPassword}</td>
    <td>{student.temporaryStudentId}</td>
  </tr>
))}
```

## Code Changes Summary

### 1. UserService.cs
- Added `GenerateTemporaryStudentId()` method
- Updated `ImportStudentsFromExcelAsync()` to generate and assign temporary IDs
- Temporary ID is stored in `StudentIdNumber` field

### 2. ImportUserDto.cs
- Added `TemporaryStudentId` property to `StudentRegistrationResult`

### 3. No Database Migration Needed
- Uses existing `StudentIdNumber` field
- No new columns required

## Testing

### Test Scenario 1: Import Students
1. Create Excel file with student names
2. Import via API
3. Verify each student has:
   - Unique username
   - Generated password
   - Temporary student ID (TEMP-YYYY-XXXXX format)

### Test Scenario 2: Login with Temporary ID
1. Use generated username and password to login
2. View student credentials
3. Verify temporary ID is displayed

### Test Scenario 3: Update to Real ID
1. Login as student
2. Complete profile with real student ID
3. Verify:
   - Student ID updated from TEMP-YYYY-XXXXX to real ID
   - Generated password still accessible
   - Can lookup student by new real ID

### Test Scenario 4: Sequential ID Generation
1. Import multiple batches of students
2. Verify temporary IDs are sequential
3. Verify no duplicate IDs across batches

## Security Considerations

1. **Password Storage**: Generated passwords are stored in plain text in `GeneratedPassword` field for initial distribution only
2. **Password Hash**: Actual authentication uses `PasswordHash` (bcrypt)
3. **Temporary Email**: Students must update to real email for account recovery
4. **Access Control**: Only admins can import students and view generated passwords

## Future Enhancements

1. **Bulk ID Update**: Admin feature to update multiple temporary IDs at once
2. **ID Expiration**: Set expiration date for temporary IDs
3. **Notification System**: Remind students to update their temporary IDs
4. **Audit Trail**: Log when temporary IDs are replaced with real IDs
