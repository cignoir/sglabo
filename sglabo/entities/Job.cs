using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sglabo.entities
{
    enum Job
    {
        戦士, 騎士, 格闘士, 幻闘士, 盗賊, 忍者, 精霊, 召喚, 黒印, 錬金, 守護, 次元
    }

    class JobConverter
    {
        public static Job ConvertFrom(string jobName)
        {
            Job job;
            switch(jobName)
            {
                case "戦士":
                    job = Job.戦士;
                    break;
                case "騎士":
                    job = Job.騎士;
                    break;
                case "格闘士":
                    job = Job.格闘士;
                    break;
                case "幻闘士":
                    job = Job.幻闘士;
                    break;
                case "盗賊":
                    job = Job.盗賊;
                    break;
                case "忍者":
                    job = Job.忍者;
                    break;
                case "精霊":
                    job = Job.精霊;
                    break;
                case "召喚":
                    job = Job.召喚;
                    break;
                case "黒印":
                    job = Job.黒印;
                    break;
                case "錬金":
                    job = Job.錬金;
                    break;
                case "守護":
                    job = Job.守護;
                    break;
                case "次元":
                    job = Job.次元;
                    break;
                default:
                    job = Job.戦士;
                    break;
            }
            return job;
        }
    }
}
