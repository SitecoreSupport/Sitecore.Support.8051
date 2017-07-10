using System.Collections.Generic;
using System.Linq;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.XA.Foundation.IoC;
using Sitecore.XA.Foundation.SitecoreExtensions.Repositories;
using Sitecore.XA.Feature.CreativeExchange.Pipelines.Import.RenderingProcessing;

namespace Sitecore.Support.XA.Feature.CreativeExchange.Pipelines.Import.RenderingProcessing
{
    public class RenderingDetailsExtractor : Sitecore.XA.Feature.CreativeExchange.Pipelines.Import.RenderingProcessing.RenderingDetailsExtractor
    {
        //protected IContentRepository ContentRepository;

        public override void Process(ImportRenderingProcessingArgs args)
        {
            ContentRepository = ServiceLocator.Current.Resolve<IContentRepository>();

            args.RenderingSourceItem = GetAttributeValueItem(args, Sitecore.XA.Feature.CreativeExchange.Constants.PageIdDataAttribute);
            args.Rendering = GetAttributeValueItem(args, Sitecore.XA.Feature.CreativeExchange.Constants.RenderingIdDataAtteibute);
            args.RenderingUniqueId = GetRenderingUniqueId(args);
            args.Page = GetPageItem(args);
        }
        /*
                protected virtual ID GetRenderingUniqueId(ImportRenderingProcessingArgs args)
                {
                    return args.Attributes.ContainsKey(Constants.UniqueIdDataAtteibute) ? ID.Parse(args.Attributes[Constants.UniqueIdDataAtteibute].First()) : null;
                }
                */
        protected virtual Item GetPageItem(ImportRenderingProcessingArgs args)
        {
            var id = args.Attributes.ContainsKey(Sitecore.XA.Feature.CreativeExchange.Constants.PageIdDataAttribute) ? ID.Parse(args.Attributes[Sitecore.XA.Feature.CreativeExchange.Constants.PageIdDataAttribute].First()) : null;
            if (args.Page.ID != id)
            {
                return args.Page.Database.GetItem(id);
            }
            return args.Page;
        }
    }
}
