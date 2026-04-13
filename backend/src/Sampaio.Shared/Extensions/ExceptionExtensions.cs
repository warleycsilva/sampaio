using System;
using System.Text;

namespace Sampaio.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetStackTraceMessage(this Exception ex)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendFormat("[{0}: {1}]", ex.GetType().Name, ex.Message);
            msg.AppendLine();
            msg.AppendLine(ex.StackTrace);

            return msg.ToString();
        }

        public static string GetCompleteMessage(this Exception ex)
        {
            ThrowArgumentNullException(ex, "ex");

            var msg = new StringBuilder();

            Exception baseEx = ex.GetBaseException();

            if (baseEx == null)
            {
                baseEx = ex;
            }

            msg.AppendFormat("Message: {0}", baseEx.Message);
            msg.AppendLine();
            msg.AppendLine();

            if (baseEx.TargetSite != null)
            {
                msg.AppendFormat("Class: {0}\n", baseEx.TargetSite.DeclaringType.Name);
                msg.AppendLine();

                msg.AppendFormat("Method: {0}", baseEx.TargetSite.Name);
                msg.AppendLine();
            }

            msg.AppendLine();
            msg.AppendLine("StackTrace:");
            msg.Append(GetStackTraceMessage(baseEx));

            return msg.ToString();
        }

        public static string GetCompleteRecursiveMessage(this Exception ex)
        {
            ThrowArgumentNullException(ex, "ex");

            var msg = new StringBuilder();

            msg.Append(GetCompleteMessage(ex));

            if (ex.InnerException != null)
            {
                InternalGetCompleteRecursiveMessage(ex.InnerException, msg);
            }

            return msg.ToString();
        }

        private static void InternalGetCompleteRecursiveMessage(this Exception ex, StringBuilder msg)
        {
            if (ex.InnerException != null)
            {
                InternalGetCompleteRecursiveMessage(ex.InnerException, msg);
                msg.AppendLine();
            }

            msg.Append(GetStackTraceMessage(ex));
        }

        private static ArgumentNullException ThrowArgumentNullException(object parameter, string parameterName)
        {
            return new ArgumentNullException(parameterName, $"The argument '{parameterName}' can not be null.");
        }
    }
}
