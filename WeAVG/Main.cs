using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace WeAvgAssembly
{

     
    [Serializable]
    [Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native, IsInvariantToNulls = true)]
    public struct WeAVG
    {
        private SqlDouble sum_;
        private SqlDouble count_;

        public void Init()
        {
            sum_ = 0;
            count_ = 0;
        }

        public void Accumulate(SqlDouble Vol, SqlDouble Ccp)
        {
            if (!(Vol.IsNull || Ccp.IsNull))
            {
                sum_ = sum_ + (Vol * Ccp);
                count_ = count_ + Vol;
            }
        }

        public void Merge(WeAVG Group)
        {
            sum_ = sum_ + Group.sum_;
            count_ = count_ + Group.count_;
        }

        public SqlDouble Terminate()
        {

            return count_ == 0 ? SqlDouble.Null : sum_ / count_;

        }
     
    }
   
}
