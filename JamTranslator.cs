using System;
using System.Collections.Generic;

namespace Jamdofai
{
    public static class JamTranslator
    {
        private static readonly char undefined = (char)0;

        private static readonly List<char> top_list = new List<char>() {
            'ㄱ',
            'ㄲ',
            'ㄴ',
            'ㄷ',
            'ㄸ',
            'ㄹ',
            'ㅁ',
            'ㅂ',
            'ㅃ',
            'ㅅ',
            'ㅆ',
            'ㅇ',
            'ㅈ',
            'ㅉ',
            'ㅊ',
            'ㅋ',
            'ㅌ',
            'ㅍ',
            'ㅎ'
        };

        private static readonly List<char> mid_list = new List<char>() {
            'ㅏ',
            'ㅐ',
            'ㅑ',
            'ㅒ',
            'ㅓ',
            'ㅔ',
            'ㅕ',
            'ㅖ',
            'ㅗ',
            'ㅘ',
            'ㅙ',
            'ㅚ',
            'ㅛ',
            'ㅜ',
            'ㅝ',
            'ㅞ',
            'ㅟ',
            'ㅠ',
            'ㅡ',
            'ㅢ',
            'ㅣ'
        };

        private static readonly List<char> bot_list = new List<char>() {
            undefined,
            'ㄱ',
            'ㄲ',
            'ㄳ',
            'ㄴ',
            'ㄵ',
            'ㄶ',
            'ㄷ',
            'ㄹ',
            'ㄺ',
            'ㄻ',
            'ㄼ',
            'ㄽ',
            'ㄾ',
            'ㄿ',
            'ㅀ',
            'ㅁ',
            'ㅂ',
            'ㅄ',
            'ㅅ',
            'ㅆ',
            'ㅇ',
            'ㅈ',
            'ㅊ',
            'ㅋ',
            'ㅌ',
            'ㅍ',
            'ㅎ'
        };

        private static char Change_mid(char word)
        {
            if (word == 'ㅔ') return 'ㅐ';
            else if (word == 'ㅐ') return 'ㅔ';
            else if (word == 'ㅙ') return 'ㅚ';
            else if (word == 'ㅚ') return 'ㅙ';
            else if (word == 'ㅖ') return 'ㅒ';
            else if (word == 'ㅒ') return 'ㅖ';
            else if (word == 'ㅛ') return 'ㅕ';
            else return word;
        }

        private static char Change_bot(char word)
        {
            if (word == 'ㅅ') return 'ㅌ';
            else if (word == 'ㅆ') return 'ㅈ';
            else if (word == 'ㅎ') return 'ㅅ';
            else if (word == 'ㅉ') return 'ㅈ';
            else return word;
        }

        private static char Second_filter(char word)
        {
            if (word == '떡') return '떻';
            else if (word == '안') return '않';
            else if (word == '괜') return '괞';
            else if (word == '찮') return '찬';
            else if (word == '떻') return '떡';
            else if (word == '송') return '성';
            else return word;
        }

        private static string Final_filter(string word)
        {
            Random random = new Random();
            string result_imoji = "";

            if (word.Contains("..."))
            { // 크라이
                int rand = (int)(random.NextDouble() * 10000 % 5);
                for (int i = -7; i < rand; i++)
                {
                    if ((int)(random.NextDouble() * 10000 % 5) == 4)
                        result_imoji += 'ㅜ';
                    result_imoji += 'ㅠ';
                }
                rand = (int)(random.NextDouble() * 10000 % 4);
                for (var i = -7; i < rand; i++)
                {
                    if ((int)(random.NextDouble() * 10000 % 5) == 4)
                        result_imoji += "😢";
                    result_imoji += "😭";
                }
            }
            else if (word.Contains("!!!"))
            { // 화남
                var rand = (int)(random.NextDouble() * 10000 % 5);
                for (var i = -7; i < rand; i++)
                {
                    if ((int)(random.NextDouble() * 10000 % 5) == 4)
                        result_imoji += '@';
                    result_imoji += ';';
                }
                rand = (int)(random.NextDouble() * 10000 % 4);
                for (var i = -7; i < rand; i++)
                {
                    if ((int)(random.NextDouble() * 10000 % 5) == 4)
                        result_imoji += '!';
                    result_imoji += "🤬";
                }
            }
            return word.Replace("아니", "않이").Replace("읽으", "일그").Replace("합니", "함미").Replace("습니", "슴미") + result_imoji;
        }

        public static int TopIndex(char word)
        {
            for (int key = 0; key < top_list.Count; key++)
            {
                if (top_list[key] == word)
                {
                    return key;
                }
            }
            return undefined;
        }

        public static int MidIndex(char word)
        {
            char replaced = Change_mid(word);
            for (int key = 0; key < mid_list.Count; key++)
            {
                if (mid_list[key] == replaced)
                {
                    return key;
                }
            }
            return undefined;
        }

        public static int BotIndex(char word)
        {
            char replaced = Change_bot(word);
            for (int key = 0; key < bot_list.Count; key++)
            {
                if (bot_list[key] == replaced)
                {
                    return key;
                }
            }
            return undefined;
        }

        public static string Translate(string content)
        {
            if (content == null)
                return content;
            Random random = new Random();
            List<char> letterList = new List<char>();
            List<char> result = new List<char>();

            for (var i = 0; i < content.Length; i++)
            {
                int unicodeDec = content[i];

                if (unicodeDec < 12593)
                {
                    letterList.Add(content[i]);
                    letterList.Add(undefined);
                    letterList.Add(undefined);
                    continue;
                }
                else if (unicodeDec < 44032)
                {
                    letterList.Add(content[i]);
                    letterList.Add(undefined);
                    letterList.Add(undefined);
                    continue;
                }

                int letterIndex = unicodeDec - 44032;

                int topIndex = letterIndex / (21 * 28);
                int midIndex = letterIndex % (21 * 28) / 28;
                int botIndex = letterIndex % (21 * 28) % 28;

                letterList.Add(top_list.Count <= topIndex ? undefined : top_list[topIndex]);
                letterList.Add(mid_list.Count <= midIndex ? undefined : mid_list[midIndex]);
                letterList.Add(bot_list.Count <= botIndex ? undefined : bot_list[botIndex]);
            }

            for (var i = 0; i < letterList.Count; i += 3)
            {
                if (((letterList[i] + "")[0] < 44032) && letterList.Count <= i + 1)
                { // 완성된 글자가 아닐 경우
                  // if(letterList[i+1] != undefined && ((letterList[i+1] + '').charCodeAt(0) < 12623)) { // 중성이 모음이 아닐 경우
                  //     if(letterList[i+2] != undefined && ((letterList[i+2] + '').charCodeAt(0) < 12623)) { // 종성이 모음이 아닐 경우
                  //         result.push(letterList[i]);
                  //         result.push(letterList[i+1]);
                  //         result.push(letterList[i+2]);
                  //         continue;
                  //     }
                  //     result.push(letterList[i]);
                  //     result.push(letterList[i+1]);
                  //     i -= 1;
                  //     continue;
                  // }
                    result.Add(letterList[i]);
                    continue;
                }

                // console.log(letterList);

                char top = letterList[i];
                char mid = letterList.Count <= i + 1 ? undefined : letterList[i + 1];
                char bot = letterList.Count <= i + 2 ? undefined : letterList[i + 2];

                if ((int)(random.NextDouble() * 10000000 % 5) == 2)
                {
                    result.Add(top);
                    if (mid != undefined)
                    {
                        result.Add(Change_mid(mid));
                    }
                    if (bot != undefined)
                    {
                        result.Add(Change_bot(bot));
                    }
                    continue;
                }
                else
                {
                    if (mid != undefined)
                    {
                        if (bot != undefined)
                        {
                            result.Add((char)(((TopIndex(top) * 21) + MidIndex(mid)) * 28 + BotIndex(bot) + 44032)); // 둘 다 있을때
                        }
                        else
                        {
                            result.Add((char)((((TopIndex(top) * 21) + MidIndex(mid)) * 28) + 44032)); // 받침만 없을 때
                        }
                    }
                    else
                    {
                        result.Add(top);
                    }
                }
            }

            string html = "";

            for (var i = 0; i < result.Count; i++)
            {
                html += Second_filter(result[i]);
            }

            return Final_filter(html);
        }
    }
}
