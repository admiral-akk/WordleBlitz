
import urllib.request as urllib2
from bs4 import BeautifulSoup
import re


def generate_simpsons_data(data):
    l = []
    for line in data.readlines():
        s = [re.sub('[()\n]', '', x) for x in  line.split(' ') if len(x) > 0]
        if len(s) != 3:
            continue
        if not s[1].isalpha():
            continue
        if not s[0].isnumeric() or not s[2].isnumeric():
            continue
        l.append((int(s[0]), s[1],int(s[2])))
    return l

def extract_simpsons_data():
    data = open("Downloads/simpsons_raw.txt", mode='r', encoding='utf-8')
    f = open("ScrapedData/simpsons.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = generate_simpsons_data(data)
    for (rank, word, count) in l:
        f.write("{0},{1},{2}\n".format(rank,word,count))
    f.close()
    data.close()