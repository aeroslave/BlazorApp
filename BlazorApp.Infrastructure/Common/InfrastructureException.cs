﻿namespace BlazorApp.Infrastructure.Common;

public class InfrastructureException : Exception
{
    public InfrastructureException(string message) : base(message)
    {
    }
}