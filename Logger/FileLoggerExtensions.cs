using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory loggerFactory, string FilePath)
        {
            loggerFactory.AddProvider(new FileLoggerProvider(FilePath));
            return loggerFactory;
        }
    }
}
