﻿using System;
using EPiServer.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JOS.ContentSerializer.Internal
{
    public class DefaultJsonContentSerializer : IContentJsonSerializer
    {
        private readonly IPropertyManager _propertyManager;

        public DefaultJsonContentSerializer(IPropertyManager propertyManager)
        {
            _propertyManager = propertyManager ?? throw new ArgumentNullException(nameof(propertyManager));
        }

        public string Serialize(IContentData contentData)
        {
            return Execute(contentData, new ContentSerializerSettings());
        }

        public string Serialize(IContentData contentData, ContentSerializerSettings settings)
        {
            return Execute(contentData, settings);
        }

        public object GetStructuredData(IContentData contentData)
        {
            return this._propertyManager.GetStructuredData(contentData, new ContentSerializerSettings());
        }

        public object GetStructuredData(IContentData contentData, ContentSerializerSettings settings)
        {
            return this._propertyManager.GetStructuredData(contentData, settings);
        }

        private string Execute(IContentData contentData, ContentSerializerSettings settings)
        {
            var result = this._propertyManager.GetStructuredData(contentData, settings);
            return JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}
