using OnionCrafter.Action.Request.Base;
using OnionCrafter.Action.Response.Base;
using OnionCrafter.Wrapper.Response.Base;

namespace OnionCrafter.Wrapper.Request.Base
{
    /// <summary>
    /// Base abstract class for defining request schema used in MediatR requests. Implements the <see cref="IRequestSchema{TKey, TResponseSchema, TReturnData, TRequestData}"/> interface.
    /// It defines generic types for Action Id, Response Schema, Return Data, and Request Data.
    /// </summary>
    /// <typeparam name="TKey">Type of Action Id.</typeparam>
    /// <typeparam name="TResponseSchema">Type of Response Schema.</typeparam>
    /// <typeparam name="TResponseData">Type of Response Data.</typeparam>
    /// <typeparam name="TRequestData">Type of Request Data.</typeparam>

    public abstract class BaseRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> : IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData>
         where TResponseSchema : IResponseSchema<TKey, TResponseData>
         where TResponseData : class, IResponseData
         where TRequestData : class, IRequestData
         where TKey : notnull, IEquatable<TKey>, IComparable<TKey>
    {
        /// <inheritdoc/>

        public TKey ActionId { get; set; }
        /// <inheritdoc/>

        public TRequestData RequestData { get; set; }
        /// <inheritdoc/>

        public bool Succeeded { get; set; }
        /// <inheritdoc/>

        public DateTime TimeStamp { get; set; }
        /// <inheritdoc/>

        public RequestType RequestType { get; set; }

        /// <summary>
        /// The feature call
        /// </summary>
        protected string featureCall;

        /// <summary>
        /// Dictionary that maps the RequestType to its corresponding implementation name.
        /// </summary>
        protected readonly Dictionary<RequestType, string> _featureImplementationNames = new Dictionary<RequestType, string>()
        {
           {RequestType.None, "Async" },
           {RequestType.Query, "Query"},
           {RequestType.Command, "Command"}
        };

        /// <summary>
        /// Constructor for setting default values for Action Id, Request Data, and feature call.
        /// </summary>
        protected BaseRequestSchema()
        {
            ActionId = Activator.CreateInstance<TKey>();
            RequestData = Activator.CreateInstance<TRequestData>();
            featureCall = string.Empty;
            SetFeatureCall();
        }

        /// <inheritdoc/>

        public string GetRequestFeature()
        {
            return featureCall;
        }

        /// <inheritdoc/>

        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetFeatureCall()
        {
            string nameOfClass = GetType().Name;
            SetFeatureCall(nameOfClass);
            return this;
        }

        /// <inheritdoc/>

        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetFeatureCall(string name)
        {
            featureCall = name;
            foreach ((var key, string value) in _featureImplementationNames)
            {
                if (name.Contains(value))
                {
                    _ = featureCall.Replace(value, string.Empty);
                    RequestType = key;
                }
            }
            return this;
        }

        /// <inheritdoc/>

        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetRequestData(TRequestData requestData)
        {
            RequestData = requestData;
            return this;
        }

        /// <inheritdoc/>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetRequestType(RequestType requestType)
        {
            RequestType = requestType;
            return this;
        }

        /// <inheritdoc/>
        public void SetActionId(TKey key)
        {
            ActionId = key;
        }

        /// <inheritdoc/>
        public abstract void CreateActionId();

        /// <summary>
        /// Sets the response as a successful result.
        /// </summary>
        /// <returns>
        /// The request schema instance.
        /// </returns>
        public IRequestSchema<TKey, TResponseSchema, TResponseData, TRequestData> SetSuccessfullyResult()
        {
            Succeeded = true;
            return this;
        }
    }
}