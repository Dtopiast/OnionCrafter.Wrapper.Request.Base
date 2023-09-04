﻿using OnionCrafter.Action.Request.Base;
using OnionCrafter.Action.Response.Base;
using OnionCrafter.Wrapper.Response.Base;

namespace OnionCrafter.Wrapper.Request.Base.Command
{
    /// <summary>
    /// Represents a base class for implementing command request schemas. Inherits from BaseRequestSchema and implements ICommandRequestSchema.
    /// </summary>
    /// <typeparam name="TKey">The type of the action ID.</typeparam>
    /// <typeparam name="TResponseSchema">The type of the response schema.</typeparam>
    /// <typeparam name="TReturnData">The type of the return data.</typeparam>
    /// <typeparam name="TRequestData">The type of the request data.</typeparam>
    public abstract class BaseCommandRequestSchema<TKey, TResponseSchema, TReturnData, TRequestData> :
        BaseRequestSchema<TKey, TResponseSchema, TReturnData, TRequestData>,
        ICommandRequestSchema<TKey, TResponseSchema, TReturnData, TRequestData>
            where TResponseSchema : IResponseSchema<TKey, TReturnData>
            where TReturnData : class, IResponseData
            where TRequestData : class, IRequestData
            where TKey : notnull, IEquatable<TKey>, IComparable<TKey>

    {
    }
}