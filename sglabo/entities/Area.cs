using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    enum Area
    {
        ルデンヌ大森林, ナビア北限地帯, シュロ陸峡,
        イレネイド山脈,
        グランドロン,
        戦豹の試練,
        猛虎の試練,
        荒獅子の試練
    }

    class AreaConverter
    {
        public static Area ConvertFrom(string areaName){
            Area area;
            switch(areaName){
                case "ルデンヌ大森林":
                    area = Area.ルデンヌ大森林;
                    break;
                case "イレネイド山脈":
                    area = Area.イレネイド山脈;
                    break;
                case "ナビア北限地帯":
                    area = Area.ナビア北限地帯;
                    break;
                case "シュロ陸峡":
                    area = Area.シュロ陸峡;
                    break;
                case "グランドロン":
                    area = Area.グランドロン;
                    break;
                case "戦豹の試練":
                    area = Area.戦豹の試練;
                    break;
                case "猛虎の試練":
                    area = Area.猛虎の試練;
                    break;
                case "荒獅子の試練":
                    area = Area.荒獅子の試練;
                    break;
                default:
                    area = Area.ルデンヌ大森林;
                    break;
            }
            return area;
        }
    }
}
