﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing
{
    public interface IDomainEvent<out TKey>
    {
        long AggregateVersion { get; }
    }
}
