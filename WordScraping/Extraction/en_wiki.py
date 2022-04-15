
def parse_en_wiki(data):
    l = []
    for line in data.readlines():
        s = line.split(' ')
        if len(s[0]) == 0:
            continue
        l.append((len(l)+1,s[0],int(s[1])))
    return l

def extract_en_wiki():
    data = open("Downloads/enwiki-20190320-words-frequency.txt", mode='r', encoding='utf-8')
    f = open("ScrapedData/en_wiki.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = parse_en_wiki(data)
    for (rank, word, count) in l:
        f.write("{0},{1},{2}\n".format(rank,word,count))
    f.close()
    data.close()