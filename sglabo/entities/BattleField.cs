using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sglabo.entities
{
    class BattleField
    {
        //ScreenPosition N;
        //ScreenPosition E;
        //ScreenPosition W;
        //ScreenPosition S;

        BattleFieldCell[,] cells;

        public int turn = 1;
        public int pcCount = 0;
        public int enemyCount = 0;

        public int rowCount { get; set; }
        public int colCount { get; set; }

        public BattleField(string map)
        {
            Load(map);
        }

        //public static string DetectBattleMapName(string areaName)
        //{
        //    var memberCount = SGWindow.sgList.Count;
        //    switch(memberCount)
        //    {
        //        case 1:
        //        case 2:
        //            break;
        //        case 3:
        //        case 4:
        //            break;
        //        case 5:
        //            break;
        //        default:
        //            break;
        //    }

        //    var rsc = Properties.Resources.ResourceManager.GetString("ルデンヌA");
        //    // エリアと人数をもとにマップ検出
        //    return rsc;
        //}

        public static string Detect(Area area)
        {
            var memberCount = SGWindow.sgList.Count;
            switch(memberCount)
            {
                case 1:
                case 2:
                    break;
                case 3:
                case 4:
                    break;
                case 5:
                    break;
                default:
                    break;
            }

            var sg = SGWindow.sgList.First();
            sg.Activate();

            Bitmap bmp = null;
            if(memberCount == 1 || memberCount == 2)
            {
                if(area == Area.ルデンヌ大森林)
                {
//573,248,40,40
//8726967	大きな木の4マス
//3832661	真ん中に一本の道3マス
//60603230	左に細い木

                    bmp = sg.CaptureRectangle(new Rectangle(573, 248, 40, 40));
                }
            }

            int mapCode = bmp != null ? GraphicUtils.GenerateUniqueCode(bmp) : 0;
            return Properties.Resources.ResourceManager.GetString("MAP" + mapCode.ToString());
        }

        public void Load(string map){
            // FIXME
            if(map == null)
            {
                map = Properties.Resources.ResourceManager.GetString("MAP60603230");
            }

            var lines = map.Split('\n');
            foreach(string line in lines)
            {
                if(line.Contains("x"))
                {
                    var elems = line.Split('x');
                    rowCount = int.Parse(elems[0]);
                    colCount = int.Parse(elems[1]);
                    cells = new BattleFieldCell[rowCount, colCount];
                }
                else
                {
                    var elems = line.Split('\t');
                    if(elems.Length == 6)
                    {
                        var gPos = new GridPosition(int.Parse(elems[0]), int.Parse(elems[1]));
                        var sPos = new ScreenPosition(int.Parse(elems[3]), int.Parse(elems[4]));
                        var height = int.Parse(elems[2]);
                        var canMove = int.Parse(elems[5]) == 1;
                        var cell = new BattleFieldCell(gPos, sPos, height, canMove);
                        cells[gPos.x, gPos.y] = cell;
                    }
                }

            }
        }

        public void Scan()
        {
            for(int row = 0; row < rowCount; row++ )
            {
                for(int col = 0; col < colCount; col++ )
                {
                    cells[row, col].Scan();
                }
            }
        }

        public void ParallelScan()
        {
            Parallel.For(0, rowCount, row =>
            {
                Parallel.For(0, colCount, col =>
                {
                    cells[row, col].Scan();
                });
            });
        }
    }
}
