using MediatR;
using OnionCrafter.Action.Base;
using OnionCrafter.Action.Request.Base;
using OnionCrafter.Action.Response.Base;
using OnionCrafter.Wrapper.Response.Base;

namespace OnionCrafter.Wrapper.Request.Base
{
    /// <summary>
    /// Enum to represent the type of request.
    /// </summary>
    public enum RequestType
    {
        /// <summary>
        /// No specific request type specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a query request.
        /// </summary>
        Query,

        /// <summary>
        /// Represents a command request.
        /// </summary>
        Command
    }

    /// <summary>
    /// Interface representing a request schema that defines the contract for a MediatR request object, as well as additional properties and methods for tracing and feature management.
    /// </summary>
    /// <typeparam name="TKey">The type of the request key.</typeparam>
    /// <typeparam name="TResponseSchema">The type of the response schema.</typeparam>
    /// <typeparam name="TResponseData">The type of the response data.</typeparam>
    /// <typeparam name="TRequestData">The type of the request data.</typeparam>
    public interface IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> :
        IRequest<TResponseSchema>,
        IBaseRequestSchema,
        IActionDetails<TKey>

        where TResponseSchema : IResponseSchema<TKey, TResponseData>
        where TResponseData : class, IResponseData
        where TRequestData : class, IRequestData
        where TKey : notnull, IEquatable<TKey>, IComparable<TKey>
    {
        /// <summary>
        /// Gets  the request data.
        /// </summary>
        TRequestData RequestData { get; protected set; }

        /// <summary>
        /// Gets the type of the request.
        /// </summary>
        public RequestType RequestType { get; protected set; }

        /// <summary>
        /// Gets the feature of the request.
        /// </summary>
        /// <returns>The name of the feature</returns>
        public string GetRequestFeature();

        /// <summary>
        /// Sets the request data of the request.
        /// </summary>
        /// <param name="requestData">The request data to set.</param>
        /// <returns>The request schema instance.</returns>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetRequestData(TRequestData requestData);

        /// <summary>
        /// Sets the feature call of the request.
        /// </summary>
        /// <returns>The request schema instance.</returns>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetFeatureCall();

        /// <summary>
        /// Sets the feature call of the request with a specific name.
        /// </summary>
        /// <param name="name">The name of the feature call to set.</param>
        /// <returns>The request schema instance.</returns>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetFeatureCall(string name);

        /// <summary>
        /// Sets the request type call of the request with a specific type.
        /// </summary>
        /// <param name="requestType">The type of the feature call to set.</param>
        /// <returns>The request schema instance.</returns>

        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetRequestType(RequestType requestType);

        /// <summary>
        /// Sets the response as a successful result.
        /// </summary>
        /// <returns>The request schema instance.</returns>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetSuccessfullyResult();
    }
}