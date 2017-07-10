using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Extensions.XElementExtensions;
using Sitecore.Mvc.Pipelines.Response.GetXmlBasedLayoutDefinition;
using Sitecore.Mvc.Presentation;
using Sitecore.Pipelines;
using Sitecore.Pipelines.ResolveRenderingDatasource;
using Sitecore.XA.Foundation.IoC;
using Sitecore.XA.Foundation.LocalDatasources.Services;
using Sitecore.XA.Feature.Composites.Pipelines.GetXmlBasedLayoutDefinition;

namespace Sitecore.Support.XA.Feature.Composites.Pipelines.GetXmlBasedLayoutDefinition
{
    public class InjectCompositeComponents : Sitecore.XA.Feature.Composites.Pipelines.GetXmlBasedLayoutDefinition.InjectCompositeComponents
    {
        public const string CompositeItemXmlAttr = "cmps-item";
        protected override void MergeComposites(XElement layoutXml, List<XElement> compositeRenderings, KeyValuePair<int, Item> composite, int dynamicPlaceholderId, string parentPh, string partialDesignId)
        {
            compositeRenderings.ForEach(compositeRendering =>
            {
                var ph = GetRelativePlaceholder(compositeRendering, composite, dynamicPlaceholderId, parentPh);
                compositeRendering.Attribute("ph").SetValue(ph);
                SetAttribute(compositeRendering, CompositeItemXmlAttr, composite.Value.ID);
                SetTrueAttribute(compositeRendering, Sitecore.XA.Feature.Composites.Constants.CompositeXmlAttr);
                HandleEmptyDatasources(composite.Value, compositeRendering);
                SetPartialDesignId(compositeRendering, partialDesignId);
                SetOriginalDataSource(compositeRendering);
            });

            var devices = layoutXml.Descendants("d").GroupBy(element => element.GetAttributeValue("id")).Select(elements => elements.First());
            foreach (var device in devices)
            {
                compositeRenderings.ForEach(compositeRendering => device.Add(compositeRendering));
            }
        }

        private void SetAttribute(XElement composite, string attribute, object value)
        {
            if (composite.Attribute(attribute) == null)
            {
                var xattr = new XAttribute(attribute, value);
                composite.Add(xattr);
            }
        }
    }
}