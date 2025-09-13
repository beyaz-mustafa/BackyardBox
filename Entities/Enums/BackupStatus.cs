namespace Entities.Enums
{
    // Represents the possible states of a backup operation
    public enum BackupStatus
    {
        // Backup is currently in progress
        SyncInProgress,

        // Backup finished successfully
        SyncCompleted,

        // Backup failed during the process
        SyncFailed,

        // No backup status assigned yet
        None
    }
}
