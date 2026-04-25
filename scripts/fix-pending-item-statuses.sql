-- ============================================================
-- fix-pending-item-statuses.sql
-- One-time data migration to sync Item."Status" for items that
-- have an active LentItems record (Pending or Approved) but
-- whose Item."Status" was never updated (still 'Available').
--
-- Run this once against the Supabase database after deploying
-- the LentItemsService fix (AddReservationAsync now sets
-- Item.Status = Unavailable on creation).
--
-- ItemStatus is stored as a string (HasConversion<string>()).
-- Safe to re-run — WHERE guards prevent double-updates.
-- ============================================================

BEGIN;

-- 1. Items with a Pending lent record → Unavailable
--    (request submitted but not yet approved)
UPDATE "Items"
SET    "Status"    = 'Unavailable',
       "UpdatedAt" = NOW()
WHERE  "Id" IN (
    SELECT DISTINCT li."ItemId"
    FROM   "LentItems" li
    WHERE  li."Status" = 'Pending'
)
AND "Status" = 'Available';   -- only touch items still showing Available

-- 2. Items with an Approved lent record → Reserved
--    (approved, awaiting physical pickup)
UPDATE "Items"
SET    "Status"    = 'Reserved',
       "UpdatedAt" = NOW()
WHERE  "Id" IN (
    SELECT DISTINCT li."ItemId"
    FROM   "LentItems" li
    WHERE  li."Status" = 'Approved'
)
AND "Status" IN ('Available', 'Unavailable');  -- catch both stale states

-- 3. Verify — show all items that have active lent records after the fix
SELECT i."Id",
       i."ItemName",
       i."Status"         AS "ItemStatus",
       li."Status"        AS "LentItemStatus",
       li."CreatedAt"     AS "LentCreatedAt"
FROM   "Items" i
JOIN   "LentItems" li ON li."ItemId" = i."Id"
WHERE  li."Status" IN ('Pending', 'Approved')
ORDER  BY li."Status", i."ItemName";

COMMIT;
