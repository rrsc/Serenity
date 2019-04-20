﻿namespace Serenity.Reporting
{
    using System;

    public class CellDecoratorAttribute : Attribute
    {
        public CellDecoratorAttribute(Type decorator)
        {
            this.DecoratorType = decorator;
        }

        public Type DecoratorType { get; set; }
    }
}
