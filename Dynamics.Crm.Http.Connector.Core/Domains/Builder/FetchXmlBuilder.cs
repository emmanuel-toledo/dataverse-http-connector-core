﻿using Dynamics.Crm.Http.Connector.Core.Utilities;
using Dynamics.Crm.Http.Connector.Core.Domains.Xml;
using Dynamics.Crm.Http.Connector.Core.Domains.Annotations;

namespace Dynamics.Crm.Http.Connector.Core.Domains.Builder
{
    /// <summary>
    /// Function to configure the FetchXml query for a Dynamics request.
    /// </summary>
    /// <typeparam name="TEntity">Custom class with "EntityAttributes" and "FieldAttributes" defined.</typeparam>
    public class FetchXmlBuilder<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Private FetchXml class instance that contains all the required properties values.
        /// </summary>
        private FetchXml _fetch = new FetchXml();

        /// <summary>
        /// Get instance of FetchXml created.
        /// </summary>
        public FetchXml Fetch { get => _fetch; }

        /// <summary>
        /// Function to add a new Filter to FetchXml query.
        /// </summary>
        /// <param name="filter">Filter builder object instance.</param>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> AddFilter(FilterBuilder<TEntity> filter)
        {
            _fetch.AddFilter(filter.FilterType, filter.Conditions);
            return this;
        }

        /// <summary>
        /// Function to add a "top" tag to FetchXml query.
        /// </summary>
        /// <param name="top">Top count records to retrive.</param>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> AddTop(int top)
        {
            _fetch.Top = top;
            return this;
        }

        /// <summary>
        /// Function to add a "distinct" tag to FetchXml query.
        /// </summary>
        /// <param name="distinct">Distinct records to retrive.</param>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> AddDistinct(bool distinct)
        {
            _fetch.Distinct = distinct;
            return this;
        }

        /// <summary>
        /// Function to add a "page" tag to FetchXml query.
        /// </summary>
        /// <param name="page">Page records to retrive.</param>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> AddPage(int page)
        {
            _fetch.Page = page;
            return this;
        }

        /// <summary>
        /// Function to add a "page size" tag to FetchXml query.
        /// </summary>
        /// <param name="pageSize">Page size records to retrive.</param>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> AddPageSize(int pageSize)
        {
            _fetch.PageSize = pageSize;
            return this;
        }

        /// <summary>
        /// Function to configure FetchXml query to retrive only one record.
        /// </summary>
        /// <returns>Same instance of FetchXml builder.</returns>
        public FetchXmlBuilder<TEntity> FirstOrDefault()
        {
            _fetch.Top = 1;
            return this;
        }

        /// <summary>
        /// Function to generate FetchXml query to use in the request.
        /// </summary>
        /// <returns>FetchXml query string.</returns>
        internal string BuildFetchXml(EntityAttributes entityAttributes, ICollection<FieldAttributes> fieldsAttributes)
            => FetchXmlBuilderUtilities.CreateEntityFetchXmlQuery<TEntity>(_fetch, entityAttributes, fieldsAttributes);

        /// <summary>
        /// Function to generate FetchXml query for entity count to use in the request.
        /// </summary>
        /// <returns>FetchXml query string.</returns>
        internal string BuildCountFetchXml(EntityAttributes entityAttributes, ICollection<FieldAttributes> fieldsAttributes)
            => FetchXmlBuilderUtilities.CreateCountFetchXmlQuery<TEntity>(_fetch, entityAttributes, fieldsAttributes);
    }
}
