-- NOTE: To manually execute this script you must
-- replace {databaseOwner} and {objectQualifier} with real values.
-- Defaults is "dbo." for database owner and "" for object qualifier

IF NOT EXISTS (select * from sys.columns where object_id = object_id(N'{databaseOwner}[{objectQualifier}MiniGallery_Images]') and name = N'OpenInLightbox')
    ALTER TABLE {databaseOwner}[{objectQualifier}MiniGallery_Images]
        ADD OpenInLightbox bit NOT NULL DEFAULT ((1))
GO
