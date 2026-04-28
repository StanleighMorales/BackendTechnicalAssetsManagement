-- Creates the ReturnSessions table
-- Run in Supabase SQL Editor: Project → SQL Editor → paste and run

CREATE TABLE IF NOT EXISTS "ReturnSessions" (
    "Id"           uuid                        NOT NULL DEFAULT gen_random_uuid(),
    "Status"       text                        NOT NULL DEFAULT 'Pending',
    "ItemName"     text                        NULL,
    "BorrowerName" text                        NULL,
    "LentItemId"   uuid                        NULL,
    "ErrorMessage" text                        NULL,
    "CreatedAt"    timestamp without time zone NOT NULL DEFAULT (now() AT TIME ZONE 'utc'),
    "UpdatedAt"    timestamp without time zone NOT NULL DEFAULT (now() AT TIME ZONE 'utc'),
    "ExpiresAt"    timestamp without time zone NOT NULL DEFAULT ((now() AT TIME ZONE 'utc') + INTERVAL '5 minutes'),

    CONSTRAINT "PK_ReturnSessions" PRIMARY KEY ("Id")
);
