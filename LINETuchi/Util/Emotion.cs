using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINETuchi.Util
{
    public enum EmotionType
    {
        Happy,
        Angry,
        Sad,
        Sleepy,
        Surprise
    }

    public class Emotion
    {
        public EmotionType Type { get; set; }
        public string KeyWord { get; set; }
        public List<string> ImgPaths { get; set; }

        private static readonly Dictionary<string, EmotionType> EmotionTypeMap = new()
        {
            { "喜", EmotionType.Happy },
            { "怒", EmotionType.Angry },
            { "哀", EmotionType.Sad },
            { "眠", EmotionType.Sleepy },
            { "驚", EmotionType.Surprise }
        };

        private static readonly Dictionary<EmotionType, (string KeyWord, List<string> ImgPaths)> EmotionData = new()
{
    { EmotionType.Happy, ("happy+cute", new List<string>
        {
            "https://cdn-ak.f.st-hatena.com/images/fotolife/i/ikenohotorino/20250208/20250208002158.jpg",
            "https://user0514.cdnw.net/shared/img/thumb/nekocyanPAKE4500-430_TP_V.jpg",
            "https://ipmag.skettt.com/_next/image?url=https%3A%2F%2Fimages.microcms-assets.io%2Fassets%2F6d0a8636a2734154af15ebf859dac810%2Fff1a9b613dd94c718e3fd63a085b71a9%2Fcat-meme-copyright-05.webp&w=3840&q=75",
            "https://www.neko-jirushi.com/img/nekosha/uploads/202502/detail/pict_255167_49523.jpg",
            "https://media.tenor.com/CnP64S7lszwAAAAi/meme-cat-cat-meme.gif",
            "https://media1.tenor.com/m/gRcOi64o3oAAAAAd/crunchycat-luna.gif",
            "https://t.pimg.jp/067/702/461/1/67702461.jpg",
            "https://i.pinimg.com/736x/4f/a0/30/4fa030f613ad2b842ac92435b597f69a.jpg",
            "https://img.benesse-cms.jp/pet-cat/item/image/normal/53663336-0514-49a5-93c2-f49c2d3c814f.jpg?w=560&h=560&resize_type=cover&resize_mode=force&p=true",
            "https://dol.ismcdn.jp/mwimgs/c/d/-/img_cded60c1d244191181a60a979aade516117313.jpg"
        })
    },
    { EmotionType.Angry, ("angry+fangs", new List<string>
        {
            "https://user0514.cdnw.net/shared/img/thumb/nekocyan458A5353_TP_V.jpg",
            "https://user0514.cdnw.net/shared/img/thumb/nekocyanPAKE4511-432_TP_V.jpg",
            "https://user0514.cdnw.net/shared/img/thumb/nekocyanPAKE5217-479_TP_V.jpg",
            "https://ipmag.skettt.com/_next/image?url=https%3A%2F%2Fimages.microcms-assets.io%2Fassets%2F6d0a8636a2734154af15ebf859dac810%2Ff70eba80b78049ee94eb6504c546e7c3%2Fcat-meme-copyright-02.webp&w=1920&q=75",
            "https://www.neko-jirushi.com/img/nekosha/uploads/202502/detail/pict_255190_49523.jpg",
            "https://media1.tenor.com/m/0eg7MZS_Q_YAAAAd/cat-cats.gif",
            "https://grapee.jp/wp-content/uploads/2021/11/74063_main1.jpg"
        })
    },
    { EmotionType.Sad, ("sad", new List<string>
        {
            "https://user0514.cdnw.net/shared/img/thumb/nekocyanPAKE4478-424_TP_V.jpg",
            "https://media1.tenor.com/m/t7_iTN0iYekAAAAd/sad-sad-cat.gif",
            "https://i.kobe-np.co.jp/rentoku/omoshiro/202301/img/b_15953396.jpg",
            "https://nekonavi.jp/wp-content/uploads/2023/04/001-17.jpeg",
            "https://img.buzzfeed.com/buzzfeed-static/static/2024-03/19/9/campaign_images/2d62f80452bc/--5-15817-1710840463-7_dblbig.jpg",
            "https://assets.st-note.com/production/uploads/images/119559787/rectangle_large_type_2_aaf2fd45606ffee34a8de95fb1a51fae.jpeg?width=1200",
            "https://cdn.p-nest.co.jp/c/nekochan.jp/pro/crop/1200x800/center/9/bcdbe0de2bd7edf60ade4ddfd6a23681.jpg",
            "https://www.crank-in.net/img/db/1135501_1200.jpg"
        })
    },
    { EmotionType.Sleepy, ("sleepy", new List<string>
        {
            "https://user0514.cdnw.net/shared/img/thumb/nyankoIMG_7270_TP_V.jpg",
            "https://user0514.cdnw.net/shared/img/thumb/shikun20220402_125829_TP_V.jpg",
            "https://www.neko-jirushi.com/img/nekosha/uploads/202502/detail/pict_255123_158470.jpg",
            "https://nyanpedia.com/wordpress/wp-content/uploads/2016/06/Fotolia_166874289_Subscription_Monthly_M.jpg",
            "https://eureka.tokyo/wp/wp-content/uploads/2024/11/sleepsleep_cat_title-1078x516.jpg",
            "https://pet-happy.jp/wp-content/uploads/2020/02/218-01_s.jpg",
            "https://nyanpedia.com/wordpress/wp-content/uploads/2017/07/akubisuruneko.jpg"
        })
    },
    { EmotionType.Surprise, ("surprise", new List<string>
        {
            "https://p.potaufeu.asahi.com/5ac3-p/picture/26605743/0374f4ca27e9a46820590b9855d79b11_640px.jpg",
            "https://www.homekikakucenter.co.jp/blog/sumi/data/img/t_20240413200534kehntb7ggaszzgqa.png",
            "https://thumb.photo-ac.com/46/465906c6447e28ecc3fb30acf12603a8_t.jpeg",
            "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR2ndB-mOBcCit_4uGp_TMkojWZ-VtF7raegw&s",
            "https://publicdomainq.net/images/202202/17s/publicdomainq-0061625ahb.jpg",
            "https://necocoto.com/wp-content/uploads/2023/11/img_odoroki.jpeg",
            "https://p.potaufeu.asahi.com/5ac3-p/picture/26605743/0374f4ca27e9a46820590b9855d79b11_640px.jpg",
            "https://www.homekikakucenter.co.jp/blog/sumi/data/img/t_20240413200534kehntb7ggaszzgqa.png"
        })
    }
};

        public Emotion(string message)
        {
            Type = GetTypeFromMessage(message);
            (KeyWord, ImgPaths) = EmotionData[Type];
        }

        private EmotionType GetTypeFromMessage(string message)
        {
            foreach (var kvp in EmotionTypeMap)
            {
                if (message.Contains(kvp.Key))
                {
                    return kvp.Value;
                }
            }
            return EmotionType.Happy;
        }
    }
}