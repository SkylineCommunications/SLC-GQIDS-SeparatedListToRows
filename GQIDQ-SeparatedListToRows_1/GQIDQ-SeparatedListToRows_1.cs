namespace GetParametersByProtocolAndView_1
{
    using System;
    using System.Collections.Generic;
    using Skyline.DataMiner.Analytics.GenericInterface;

    [GQIMetaData(Name = "Get Parameters by View")]
    public class MyDataSource : IGQIDataSource, IGQIInputArguments
    {
        private GQIStringArgument separatedListArgument = new GQIStringArgument("Separated List") { IsRequired = true };
        private GQIStringArgument separatorArgument = new GQIStringArgument("Separator") { IsRequired = true, DefaultValue = ";" };

        private string separatedList;
        private string separator;

        private List<GQIColumn> _columns;

        public GQIColumn[] GetColumns()
        {
            return _columns.ToArray();
        }

        public GQIArgument[] GetInputArguments()
        {
            return new GQIArgument[] { separatedListArgument, separatorArgument };
        }

        public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
        {
            _columns = new List<GQIColumn>
              {
                 new GQIStringColumn("ID"),
              };
            separatedList = args.GetArgumentValue(separatedListArgument);
            separator = args.GetArgumentValue(separatorArgument);

            return new OnArgumentsProcessedOutputArgs();
        }

        public GQIPage GetNextPage(GetNextPageInputArgs args)
        {
            var rows = new List<GQIRow>();

            string[] IDs = separatedList.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string ID in IDs)
            {
                rows.Add(new GQIRow(new GQICell[] { new GQICell() { Value = ID } }) { });
            }

            return new GQIPage(rows.ToArray())
            {
                HasNextPage = false,
            };
        }


    }
}