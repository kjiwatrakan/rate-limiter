﻿using System;
using System.Collections.Generic;
using System.Linq;
using RateLimiter.Model;

namespace RateLimiter.Rule
{
    public class MaxRequestsPerPeriodRule : RateLimitRuleBase
    {
        public MaxRequestsPerPeriodRule(int maxCount, TimeSpan duration)
        {
            MaxCount = maxCount;
            Duration = duration;
        }

        public int MaxCount { get; }
        public TimeSpan Duration { get; }

        public override bool CheckRequestAllow(IEnumerable<RateLimitRequest> requestData)
        {
            var beginValidationTimestamp = DateTime.UtcNow.Add(-this.Duration);

            return MaxCount <= 0 
                || Duration <= TimeSpan.Zero
                || (requestData ?? Enumerable.Empty<RateLimitRequest>())
                .Where(rd => rd.Timestamp >= beginValidationTimestamp)
                .Count() < this.MaxCount;
        }
    }
}
