
import urllib.request as urllib2
from bs4 import BeautifulSoup
import re
def generate_url():
    return "https://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/Contemporary_poetry"

def extract_word_usage(l, url):
    page = urllib2.urlopen(url)
    print(url)
    soup = BeautifulSoup(page, 'html.parser')
    for item in soup.find_all('ol'):
        children = item.find_all('li', recursive=False)
        for child in children:
            l.append((len(l)+1,child.text))
    return l

def generate_poetry_data():
    url = generate_url()
    l = []
    extract_word_usage(l, url)
    return l

def extract_poetry():
    f = open("ScrapedData/contemporary_poetry.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = generate_poetry_data()
    for (rank, word) in l:
        f.write("{0},{1}\n".format(rank,word))
    f.close()