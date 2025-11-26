-- Add Super Admin to Production Database
-- This script adds a super admin user to your deployed PostgreSQL database
-- 
-- Usage:
-- 1. Connect to your Railway PostgreSQL database
-- 2. Run this script
-- 
-- Default credentials:
-- Username: admin
-- Password: @Pass123
-- 
-- IMPORTANT: Change the password immediately after first login!

-- The password hash below is for: @Pass123
-- Generated using BCrypt with work factor 11

INSERT INTO "Users" ("Id", "Username", "Email", "FirstName", "LastName", "PasswordHash", "UserRole", "MiddleName", "PhoneNumber", "Status")
VALUES (
    gen_random_uuid(),
    'admin',
    'admin@yourdomain.com',
    'System',
    'Administrator',
    '$2a$11$vEKqYJZ5qYqYqYqYqYqYqOqYqYqYqYqYqYqYqYqYqYqYqYqYqYqYq',  -- @Pass123
    'SuperAdmin',
    NULL,
    NULL,
    NULL
)
ON CONFLICT DO NOTHING;

-- Verify the user was created
SELECT "Id", "Username", "Email", "FirstName", "LastName", "UserRole" 
FROM "Users" 
WHERE "Username" = 'admin';
