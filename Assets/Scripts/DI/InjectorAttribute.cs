﻿using System;

namespace  DI
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectorAttribute : Attribute
    {
    }
}
