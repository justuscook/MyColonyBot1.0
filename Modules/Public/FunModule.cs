using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyBot.Modules.Public
{

    public class FunModule : ModuleBase
    {
        Random rand = new Random();
        public static int hiCount;
        public static SocketGuildUser lastUserHi;


        string[] sayHi = new string[]
            {
                "Hi!","Hello there!","Hows it going?","When did you get here?","Hows it hanging?",
                "Hey.", "Wuzzzzzz Upppppp!", "Greetings and Salutations!", "I Love you!","Hola!", "Evenin' Gvna'.",
                "Good morning!", "Top of the mornin' to you!",
            };

        string[] angryHi = new string[]
        {
            "**I SAID HI!!!**", ".....Really.  Again.  Hi.", "What do you want now?", "*sigh* \n Yes."
            ,"Oh boy, why don't you bugger off!", "Seriously, there's other people here you can pester.",
        };

        string[] freshestMemes = new string[]
            {
                "http://www.madaboutmemes.com/uploads/memes/570.png",
                "http://www.madaboutmemes.com/uploads/memes/591.png",
                "http://www.madaboutmemes.com/uploads/memes/592.png",
                "http://www.madaboutmemes.com/uploads/memes/594.png",
                "https://cdn.discordapp.com/attachments/288448241479974922/292705137158258688/Speechless_Stick_Guy_18032017130446.jpg",
                "https://cdn.discordapp.com/attachments/288448241479974922/292705763976151041/Arthurs_Fist_18032017130802.jpg",
                "https://cdn.discordapp.com/attachments/288448241479974922/292708329711730688/Crying_Michael_Jordan_18032017131703.jpg",
                "https://cdn.discordapp.com/attachments/288448241479974922/292708345637240834/Suspicious_Dog_18032017131810.jpg",
                "https://cdn.discordapp.com/attachments/288448241479974922/292709264022044691/I_find_your_lack_of_faith_disturbing_18032017132051.jpg",
                "https://cdn.discordapp.com/attachments/288448241479974922/292709284024811520/Scumbag_Stacy_18032017132154.jpg",
                "http://www.madaboutmemes.com/uploads/memes/595.png",
                "https://i.imgflip.com/1m56ef.jpg",
                "https://i.imgflip.com/1m56cq.jpg",
                "https://i.imgflip.com/1m56ah.jpg",
                "https://i.imgflip.com/1m567w.jpg",
                "https://i.imgflip.com/1m5c0l.jpg",
                "https://i.imgflip.com/1m55t4.jpg",
                "https://i.imgflip.com/1m55ra.jpg",
                "https://i.imgflip.com/1m55pb.jpg",
                "https://i.imgflip.com/1m55mf.jpg",
                "https://i.imgflip.com/1m55ku.jpg",
                "https://i.imgflip.com/1m55ku.jpg",
                "https://i.imgflip.com/1m55gw.jpg",
                "https://i.imgflip.com/1m55fw.jpg",
                "https://i.imgflip.com/1m55ej.jpg",
                "https://i.imgflip.com/1m55cp.jpg",
                "https://i.imgflip.com/1m55am.jpg",
            };
        string[] beers = new string[]
            {
                "https://www.wired.com/wp-content/uploads/2015/12/bud-light-1200x601.jpg",
                "https://i.kinja-img.com/gawker-media/image/upload/s--2I0Mk6aj--/lh5dw52636ym537atvag.jpg",
                "http://gaia.adage.com/images/bin/image/jumbo/bud_light_summary_art.jpg",
                "https://assets.entrepreneur.com/static/20151222015158-budlight-chart-beers.jpg",
                "http://lifecdn.dailyburn.com/life/wp-content/uploads/2013/07/Beer_Bud-Light_2.jpg",
                "http://media1.s-nbcnews.com/i/newscms/2015_18/514261/bud-light-beer-today-150428-tease_98f03e2014c9f4fa215d31782533355e.jpg",
                "https://i2.wp.com/nevbev.com/wp-content/uploads/2015/03/budlight.jpg",
                "https://s-media-cache-ak0.pinimg.com/736x/af/31/28/af3128e2d2f76ea8ed50afbe5c05345e.jpg",
                "https://a.fastcompany.net/multisite_files/fastcompany/imagecache/1280/poster/2016/11/3065933-poster-p-1-budlight-gold.jpg",
                "http://a57.foxnews.com/images.foxnews.com/content/fox-news/food-drink/2017/02/01/kansas-man-wins-super-bowl-tickets-for-life-after-finding-golden-bud-light-can/_jcr_content/par/featured_image/media-0.img.jpg/0/0/1485974217753.jpg",
                "http://68.media.tumblr.com/9311cf7922e191260d8a0163137218d7/tumblr_nxzoq6BPKB1t1165ko1_500.jpg",
                "https://www.pikfly.com/images/products/141/21015.jpg",
                "http://1.bp.blogspot.com/--3L_9dCo0I0/VMn5-gjS9DI/AAAAAAAB4Yw/9a_fhuRfESw/s1600/BudLight-superbowl%2B(0).jpg",
                "http://www.shespeaks.com/pages/img/review/woodchuck_hard_cider_03272013104142.jpg",
                "http://i.huffpost.com/gen/913521/images/o-SHINER-BOCK-facebook.jpg",
                "http://media.graytvinc.com/images/810*810/ANGRY.ORCHARD.png",
                "http://cdn.barmano.com/recipe-images/cosmopolitan-cocktail-56-big.jpg",
                "https://static-ssl.businessinsider.com/image/56d054c32e526553008b9d67-960-720/today-guinness-is-looking-back-at-its-history-for-inspiration-for-new-brews.jpg",
                "http://l7.alamy.com/zooms/981434187eaf471d98f77ab3a9e38946/a-bottle-of-hobgoblin-real-ale-from-the-wychwood-brewery-hand-crafted-b7bbnx.jpg",
                "http://www.hollandsportsclub.co.uk/wp-content/uploads/2015/09/londonpride.jpg",
                "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/1664.beer1.JPG/220px-1664.beer1.JPG",
                "http://1.bp.blogspot.com/-Z6W0pz3EBBI/UxD_bJABLNI/AAAAAAAACmk/7Ty6XJqUOFc/s1600/Black+Sheep+Ale.jpg",
                "https://d2y0alfzxugzvv.cloudfront.net/uploads/2014/12/1417804068-carling-pint.jpg",
                "https://www.drinkupessex.uk/wp-content/uploads/2015/08/robinsons-summer-fruits-squash-1-litre-300x300.jpg",
                "https://www.sharpsbrewery.co.uk/media/catalog/category/bottle_doom_bar_2015.png",
                "https://www.ocado.com/productImages/418/41874011_0_640x640.jpg?identifier=9a87683472ac96ddf333ae9d7ca68a71",
                "http://www.winershop.com/5007-large_default/cerveja-heineken-garrafa-33cl.jpg",
                "http://www.drinkstuff.com/productimg/55191_large.jpg",
                "http://stowfordpress.co.uk/wp-content/uploads/2015/11/bottleglass.jpg",
                "http://fetchthedrinks.com/wp-content/uploads/2014/06/thistly-cross-whiskey.jpg",
                "http://www.allencrawford.net/wp-content/uploads/portraitshortcopy.jpg",
                "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f3/Bulmers_Original_Bottle.jpg/220px-Bulmers_Original_Bottle.jpg",
                "https://drinks-dvq6ncf.netdna-ssl.com/wordpress/wp-content/uploads/2015/04/magners.jpg",
                "https://img.tesco.com/Groceries/pi/414/5014201655414/IDShot_540x540.jpg",
                "http://www.stellaartois.com/en_gb/stella-artois-cidre/cidre/_jcr_content/contentPar/section_1/normal-section-content/grid/g51/image.img.png/stella-artois-apple-cidre.png",
            };
        [Command("hi")]
        [Alias("hello")]
        [Remarks("Say Hi to the bot and it will say something back")]
        public async Task hi()
        {
            var result = Database.CheckExistingServer(Context.Guild.Id);
            if (result.Count() <= 0)
            {
                Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
            }
            result = Database.CheckHiSwitch(Context.Guild.Id);
            if (result.First() == "1")
            {
                string revenge = Context.User.Mention + " HELLO!\n";


                if (lastUserHi == Context.User as SocketGuildUser)
                {
                    hiCount = hiCount + 1;
                }
                else
                {
                    lastUserHi = Context.User as SocketGuildUser;
                    hiCount = 0;
                }
                if (hiCount <= 5)
                {
                    int randomNumber = rand.Next(0, sayHi.Length - 1);
                    await Context.Channel.SendMessageAsync(Context.User.Mention + " " + sayHi[randomNumber]);
                }
                else if (hiCount > 5 && hiCount < 8)
                {
                    int randomNumber = rand.Next(0, angryHi.Length - 1);
                    await Context.Channel.SendMessageAsync(Context.User.Mention + " " + angryHi[randomNumber]);
                }
                else if (hiCount == 8)
                {
                    await Context.Channel.SendMessageAsync(Context.User.Mention + "**IF YOU SAY HI AGAIN I SWEAR..**");
                }
                else if (hiCount == 9)
                {
                    await Context.Channel.SendMessageAsync(Context.User.Mention + "** mother %*&^#`! Let's see how you like this!!!**");
                    for (int i = 0; i < 10; i++)
                    {
                        revenge = revenge + Context.User.Mention + " HELLO!\n";
                    }
                    await Context.Channel.SendMessageAsync(revenge);
                }
            }
            else
            {
                await ReplyAsync("The admins have turned the -hi command off, sorry bro.");
            }
        }

        [Command("meme")]
        [Remarks("Teh freshest my colony memes around boi!1!!")]
        public async Task meme()
        {
            var result = Database.CheckExistingServer(Context.Guild.Id);
            if (result.Count() <= 0)
            {
                Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
            }
            result = Database.CheckMemeSwitch(Context.Guild.Id);
			if (result.First() == "1")
			{
				int randomNumber = rand.Next(0, freshestMemes.Length - 1);
				var embed = new EmbedBuilder();
				embed.ImageUrl = freshestMemes[randomNumber];
				embed.WithColor(new Color(255, 255, 0));
				await Context.Channel.SendMessageAsync("", embed: embed);
			}
			else
			{
				await ReplyAsync("The server admins have turned the -meme command off, :weary:");
			}
		}
        [Command("beerme")]
        [Alias("beer me!")]
        [Remarks("If you get bored idling just get hammered!")]
        public async Task beerme()
        {
            var result = Database.CheckExistingServer(Context.Guild.Id);
            if (result.Count() <= 0)
            {
                Database.EnterServer(Context.Guild.Id, Context.Guild.Name);
            }
            result = Database.CheckBeerMeSwitch(Context.Guild.Id);
			if (result.First() == "1")
			{
				int randomNumber = rand.Next(0, beers.Length - 1);
				var embed = new EmbedBuilder();
				embed.ImageUrl = beers[randomNumber];
				embed.WithColor(new Color(255, 0, 255));
				await Context.Channel.SendMessageAsync("", embed: embed);
			}
			else
			{
				await ReplyAsync("The server admins have turned the -beerme command off, sorry mate your cut off.");
			}



		}
	}
}
