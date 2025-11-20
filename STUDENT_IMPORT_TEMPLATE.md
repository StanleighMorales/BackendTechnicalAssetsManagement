# Student Import Template Guide

## Overview
This guide explains how to bulk import students using an Excel file.

## Excel File Requirements

### File Format
- **Supported formats**: `.xlsx` or `.xls`
- **Required columns**: `LastName`, `FirstName`
- **Optional columns**: `MiddleName`

### Column Names (Case-insensitive)
The system accepts flexible column naming:

| Required | Accepted Column Names |
|----------|----------------------|
| ✓ | `LastName`, `Last Name`, `Surname` |
| ✓ | `FirstName`, `First Name`, `Given Name` |
| ✗ | `MiddleName`, `Middle Name` |

### Sample Excel Structure

| LastName | FirstName | MiddleName |
|----------|-----------|------------|
| Dela Cruz | Juan | Santos |
| Garcia | Maria | Reyes |
| Gonzales | Pedro | |
| Rodriguez | Ana | Lopez |

## What Gets Auto-Generated

For each student in the Excel file, the system automatically generates:

1. **User ID**: Unique GUID
2. **Username**: Generated from name (e.g., `juan.santos.delacruz`)
   - Format: `firstname.middlename.lastname` or `firstname.lastname`
   - Duplicates get numbered suffix (e.g., `juan.delacruz1`)
3. **Password**: Random 12-character secure password
4. **Email**: Temporary email (`username@temporary.com`)
5. **Phone Number**: Temporary placeholder (`0000000000`)
6. **User Role**: Set to `Student`
7. **Status**: Set to `Offline`

## Fields to be Completed Later

Students must complete their profile using the "Complete Registration" endpoint with:

- **Email**: Valid email address
- **Phone Number**: 10-digit phone number
- **Student ID Number**: Official student ID
- **Course**: e.g., "Computer Science"
- **Section**: e.g., "A"
- **Year**: e.g., "3"
- **Address**: Street, City/Municipality, Province, Postal Code
- **ID Pictures**: Front and back of student ID (required)
- **Profile Picture**: Optional

## API Endpoint

**POST** `/api/v1/users/students/import`

**Authorization**: Admin or Staff role required

**Content-Type**: `multipart/form-data`

**Request Body**:
```
file: students.xlsx
```

## Response Format

```json
{
  "success": true,
  "message": "Import completed. Success: 4, Failed: 0",
  "data": {
    "totalProcessed": 4,
    "successCount": 4,
    "failureCount": 0,
    "registeredStudents": [
      {
        "fullName": "Juan Santos Dela Cruz",
        "username": "juan.santos.delacruz",
        "generatedPassword": "aB3$xY9@mK2!"
      },
      {
        "fullName": "Maria Reyes Garcia",
        "username": "maria.reyes.garcia",
        "generatedPassword": "pQ7#wE5@nL8!"
      }
    ],
    "errors": []
  }
}
```

## Important Notes

1. **Save Generated Passwords**: The generated passwords are only shown once during import. Make sure to save them and distribute to students securely.

2. **Temporary Data**: Email and phone number are set to temporary values. Students MUST update these through the "Complete Registration" endpoint.

3. **Username Uniqueness**: The system automatically handles duplicate usernames by appending numbers.

4. **Error Handling**: If a row fails to import, the system continues with other rows and reports errors in the response.

## Next Steps After Import

1. Distribute usernames and passwords to students securely
2. Students log in with generated credentials
3. Students complete their profile using PATCH `/api/v1/users/students/complete-registration/{id}`
4. Students can then update their profile anytime using PATCH `/api/v1/users/students/profile/{id}`
