using System;
using Sitecore.XA.Feature.CreativeExchange.Helpers;
using Sitecore.XA.Foundation.MarkupDecorator.Pipelines.DecorateRendering;
using Sitecore.XA.Feature.CreativeExchange.Pipelines.Export.DecorateRendering;
using Sitecore.XA.Foundation.MarkupDecorator.Base;

namespace Sitecore.Support.XA.Feature.CreativeExchange.Pipelines.Export.DecorateRendering
{
    public class AddPageId : Sitecore.XA.Feature.CreativeExchange.Pipelines.Export.DecorateRendering.AddPageId
    {
        public const string CompositeItemXmlAttr = "cmps-item";
        public override void Process(RenderingDecoratorArgs args)
        {
            if (CreativeExchangeUtils.Exporting)
            {
                var compositeItemId = args.Rendering.Properties[CompositeItemXmlAttr];
                if (compositeItemId == null)
                {
                    args.AddAttribute(Sitecore.XA.Feature.CreativeExchange.Constants.PageIdDataAttribute, args.Rendering.IsFromSnippet ? args.Rendering.SnippetId.ToString() : args.PageContext.Current.ID.ToString());
                }
                else
                {
                    args.AddAttribute(Sitecore.XA.Feature.CreativeExchange.Constants.PageIdDataAttribute, args.Rendering.IsFromSnippet ? args.Rendering.SnippetId.ToString() : compositeItemId);
                }
            }
        }
    }
}
