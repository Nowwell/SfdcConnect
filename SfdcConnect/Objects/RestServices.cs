/****************************************************************************
*
*   File name: DataObjects\RestServices.cs
*   Author: Sean Fife
*   Create date: 5/21/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Includes RestServices class, the api version available @ /services/data/
*
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfdcConnect.Objects
{
    public class RestServices
    {
        public string label;
        public string url;
        public string version;
    }

}
