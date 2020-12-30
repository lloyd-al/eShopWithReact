using System;
using eShopWithReact.Common.Core.Interfaces;

namespace eShopWithReact.Common.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
