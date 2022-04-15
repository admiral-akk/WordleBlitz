
import urllib.request as urllib2
from bs4 import BeautifulSoup
import re
def generate_tv_script_url(start, end):
    return "https://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/TV/2006/" + str(start) + "-" + str(end)

def generate_thousand_tv_script_url(end):
    return generate_tv_script_url(end-999, end)

def generate_two_thousand_tv_script_url(end):
    return generate_tv_script_url(end-1999, end)

def generate_final_tv_script_url():
    return generate_tv_script_url(40001, 41284)

def extract_word_usage_pair_tv_script(url):
    page = urllib2.urlopen(url)
    print(url)

    usage = []
    soup = BeautifulSoup(page, 'html.parser')
    for item in soup.find_all('tr'):
        children = item.find_all('td', recursive=False)
        if len(children) != 3:
            continue
        if not children[0].text.isnumeric():
            continue
        word = children[1].text
        if " " in word:
            continue
        usage.append((int(children[0].text), children[1].text, int(re.sub('[^0-9]','', children[2].text))))
    return usage

def generate_tv_scripts_data():
    thousands = [1000*x for x in range(1,11)]
    twothousands = [2000*x for x in range(11,21)]
    l = []
    for n in thousands:
        url = generate_thousand_tv_script_url(n)
        l = l+ extract_word_usage_pair_tv_script(url)
    for n in twothousands:
        url = generate_two_thousand_tv_script_url(n)
        l = l+ extract_word_usage_pair_tv_script(url)
    l = l + extract_word_usage_pair_tv_script(generate_final_tv_script_url())
    return l

def extract_tv_scripts():
    f = open("ScrapedData/tv_scripts.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = generate_tv_scripts_data()
    for (rank, word, count) in l:
        f.write("{0},{1},{2}\n".format(rank,word,count))
    f.close()