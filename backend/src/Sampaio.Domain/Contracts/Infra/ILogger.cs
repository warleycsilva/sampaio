using System;

namespace Sampaio.Domain.Contracts.Infra
{
    public interface ILogger
    {
        void Info(string message);
        void Info(string message, params object[] args);
        void Warning(string message);
        void Warning(string message, params object[] args);
        void Error(Exception e);
        void Error(string message, Exception e);
        void Fatal(Exception e);
        void Fatal(string message, Exception e);
    }
}