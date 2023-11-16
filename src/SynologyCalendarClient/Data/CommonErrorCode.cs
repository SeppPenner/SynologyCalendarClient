// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonErrorCode.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//  The common error code.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SynologyCalendarClient.Data;

/// <summary>
/// The common error code.
/// </summary>
public enum CommonErrorCode
{
    /// <summary>
    /// Unknown error.
    /// </summary>
    UnknownError = 100,

    /// <summary>
    /// No parameter of API, method, or version.
    /// </summary>
    NoParameter = 101,

    /// <summary>
    /// The requested API does not exist.
    /// </summary>
    ApiNotFound = 102,

    /// <summary>
    /// The requested method does not exist.
    /// </summary>
    MethodNotFound = 103,

    /// <summary>
    /// The requested version does not support the functionality.
    /// </summary>
    UnsupportedVersion = 104,

    /// <summary>
    /// The logged in session does not have permission.
    /// </summary>
    PermissionDenied = 105,

    /// <summary>
    /// Session timeout.
    /// </summary>
    SessionTimeout = 106,

    /// <summary>
    /// Session interrupted by duplicate login.
    /// </summary>
    DuplicateLogin = 107,

    /// <summary>
    /// Missing required parameters.
    /// </summary>
    MissingParameters = 114,

    /// <summary>
    /// Unknown internal error.
    /// </summary>
    InternalError = 117,

    /// <summary>
    /// Invalid parameter.
    /// </summary>
    InvalidParameter = 120,

    /// <summary>
    /// Insufficient application privilege.
    /// </summary>
    InsufficientPrivilege = 160,

    /// <summary>
    /// Invalid parameter of file operation.
    /// </summary>
    InvalidParameterFileOperation = 400,

    /// <summary>
    /// Unknown error of file operation.
    /// </summary>
    UnknownErrorFileOperation = 401,

    /// <summary>
    /// System is too busy.
    /// </summary>
    SystemTooBusy = 402,

    /// <summary>
    /// This user does not have permission to execute this operation.
    /// </summary>
    PermissionDeniedUser = 403,

    /// <summary>
    /// This group does not have permission to execute this operation.
    /// </summary>
    PermissionDeniedGroup = 404,

    /// <summary>
    /// This user/group does not have permission to execute this operation.
    /// </summary>
    PermissionDeniedUserOrGroup = 405,

    /// <summary>
    /// Cannot obtain user/group information from the account server.
    /// </summary>
    CannotObtainUserInfo = 406,

    /// <summary>
    /// Operation not permitted.
    /// </summary>
    OperationNotPermitted = 407,

    /// <summary>
    /// No such file or directory.
    /// </summary>
    NoSuchFileOrDirectory = 408,

    /// <summary>
    /// File system not supported.
    /// </summary>
    FileSystemNotSupported = 409,

    /// <summary>
    /// Failed to connect internet-based file system (ex: CIFS).
    /// </summary>
    FailedToConnectInternetFileSystem = 410,

    /// <summary>
    /// Read-only file system.
    /// </summary>
    ReadOnlyFileSystem = 411,

    /// <summary>
    /// Filename too long in the non-encrypted file system.
    /// </summary>
    FilenameTooLongNonEncrypted = 412,

    /// <summary>
    /// Filename too long in the encrypted file system.
    /// </summary>
    FilenameTooLongEncrypted = 413,

    /// <summary>
    /// File already exists.
    /// </summary>
    FileAlreadyExists = 414,

    /// <summary>
    /// Disk quota exceeded.
    /// </summary>
    DiskQuotaExceeded = 415,

    /// <summary>
    /// No space left on device.
    /// </summary>
    NoSpaceLeftOnDevice = 416,

    /// <summary>
    /// Input/output error.
    /// </summary>
    InputOutputError = 417,

    /// <summary>
    /// Illegal name or path.
    /// </summary>
    IllegalNameOrPath = 418,

    /// <summary>
    /// Illegal file name.
    /// </summary>
    IllegalFileName = 419,

    /// <summary>
    /// Illegal file name on FAT file system.
    /// </summary>
    IllegalFileNameFat = 420,

    /// <summary>
    /// Device or resource busy.
    /// </summary>
    DeviceOrResourceBusy = 421,

    /// <summary>
    /// No such task of the file operation.
    /// </summary>
    NoSuchFileOperationTask = 599

    // Todo: Other error codes for login e.g.?!
}
