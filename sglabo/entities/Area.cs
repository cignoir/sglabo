﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    enum Area
    {
        ルデンヌ大森林, ナビア北限地帯, シュロ陸峡
    }

    class AreaConverter
    {
        public static Area ConvertFrom(string areaName){
            Area area;
            switch(areaName){
                case "ルデンヌ大森林":
                    area = Area.ルデンヌ大森林;
                    break;
                case "ナビア北限地帯":
                    area = Area.ナビア北限地帯;
                    break;
                case "シュロ陸峡":
                    area = Area.シュロ陸峡;
                    break;
                default:
                    area = Area.ルデンヌ大森林;
                    break;
            }
            return area;
        }
    }
}