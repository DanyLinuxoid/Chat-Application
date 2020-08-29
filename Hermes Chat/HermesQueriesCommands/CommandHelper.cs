using Dapper;
using System.Collections.Generic;

namespace HermesQueriesCommands
{
    public  static class CommandHelper
    {
        public static DynamicParameters GetDynamicParameters(Dictionary<string, object> userParameters, Dictionary<string, object> defaultParameters)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();

            foreach (KeyValuePair<string, object> defaultPair in defaultParameters)
            {
                if (userParameters.ContainsKey(defaultPair.Key))
                {
                    userParameters.TryGetValue(defaultPair.Key, out object value);
                    dynamicParameters.Add("@" + defaultPair.Key, value);
                }
                else
                {
                    dynamicParameters.Add("@" + defaultPair.Key, defaultPair.Value);
                }
            }

            return dynamicParameters;
        }
    }
}