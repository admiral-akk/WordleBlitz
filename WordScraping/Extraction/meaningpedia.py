
import urllib.request as urllib2
from bs4 import BeautifulSoup
from string import ascii_lowercase

import re

def get_url(c):
    return "https://meaningpedia.com/words-that-start-with-" + c + "?show=all"


def get_words(c):
    url = get_url(c)
    page = urllib2.urlopen(url)
    print(url)

    soup = BeautifulSoup(page, 'html.parser')
    return soup.find_all('span', attrs={'itemprop': 'name'})


def extract_meaningpedia():
    f = open("ScrapedData/meaningpedia.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = []
    for c in ascii_lowercase:
        words = get_words(c)
        for word in words:
            l.append(word.text)
    l.sort(key=lambda word: (len(word),word))
    for word in l:
        f.write(word + "\n")
    f.close()