using System;

namespace ValidationPilotServices.LoggerService
{
    [Obsolete]
    public static class LoggerService
    {
        public static void LoggerServiceIni(string packageDir)
        {
        }

        /// <summary>
        /// This function returns Logger to write global application information.
        /// </summary>
        /// <returns>The Package application Logger.</returns>
        public static ILog GetValidationProcessingLog() => throw new NotSupportedException("LoggerService is deprecated.");

        /// <summary>
        /// This function returns Logger to write file validation information in the package folder.
        /// </summary>
        /// <returns>The file Logger.</returns>
        public static ILog GetValidationErrorsLog() => throw new NotSupportedException("LoggerService is deprecated.");

        public static ILog GetGlobalLog() => throw new NotSupportedException("LoggerService is deprecated.");
    }

    public interface ILog
    {
        void Debug(string message);

        void Info(string message);

        void Warn(string message, Exception exception = null);

        void Error(string message, Exception exception = null);

        void Fatal(string message);
    }
}
