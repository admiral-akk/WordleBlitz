
import urllib.request as urllib2
from bs4 import BeautifulSoup
import re
def generate_gutenberg_url(start, end):
    return "https://en.wiktionary.org/wiki/Wiktionary:Frequency_lists/PG/2005/08/" + str(start) + "-" + str(end)

def generate_ten_thousand_gutenberg_url(end):
    return generate_gutenberg_url(end-9999, end)

def generate_gutenberg_urls():
    return [generate_ten_thousand_gutenberg_url(10000*x) for x in range(1,11)]

def extract_word_usage_pair_gutenberg(l, url):
    page = urllib2.urlopen(url)
    print(url)
    soup = BeautifulSoup(page, 'html.parser')
    for item in soup.find_all('p'):
        children = item.find_all('a', recursive=False)
        for child in children:
            if not child.has_attr('href'):
                continue
            if not child.has_attr('title'):
                continue
            l.append((len(l)+1,child.text))
    return l

def generate_gutenberg_data():
    urls = generate_gutenberg_urls()
    l = []
    for url in urls:
        extract_word_usage_pair_gutenberg(l, url)
    return l

def extract_gutenberg():
    f = open("ScrapedData/gutenberg.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = generate_gutenberg_data()
    for (rank, word) in l:
        f.write("{0},{1}\n".format(rank,word))
    f.close()