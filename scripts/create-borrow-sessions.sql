-- Creates the BorrowSessions table
-- Run this directly against your PostgreSQL database if you are not using EF migrations.
--
-- Usage (psql):
--   psql -h <host> -U <user> -d <database> -f scripts/create-borrow-sessions.sql

CREATE TABLE IF NOT EXISTS "BorrowSessions" (
    "Id"           uuid                        NOT NULL DEFAULT gen_random_uuid(),
    "Status"       text                        NOT NULL DEFAULT 'Pending',
    "StudentName"  text                        NULL,
    "ItemName"     text                        NULL,
    "LentItemId"   uuid                        NULL,
    "ErrorMessage" text                        NULL,
    "CreatedAt"    timestamp without time zone NOT NULL DEFAULT (now() AT TIME ZONE 'utc'),
    "UpdatedAt"    timestamp without time zone NOT NULL DEFAULT (now() AT TIME ZONE 'utc'),
    "ExpiresAt"    timestamp without time zone NOT NULL DEFAULT ((now() AT TIME ZONE 'utc') + INTERVAL '5 minutes'),

    CONSTRAINT "PK_BorrowSessions" PRIMARY KEY ("Id")
);
