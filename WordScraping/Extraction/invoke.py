
def parse_invoke(data):
    l = []
    for line in data.readlines():
        s = line.split(' ')
        if len(s[0]) == 0:
            continue
        l.append((len(l)+1,s[0],int(s[1])))
    return l

def extract_invoke():
    data = open("Downloads/en_full_invokeit.txt", mode='r', encoding='utf-8')
    f = open("ScrapedData/invokeit.txt", mode='w', encoding='utf-8')
    f.truncate(0)
    l = parse_invoke(data)
    for (rank, word, count) in l:
        f.write("{0},{1},{2}\n".format(rank,word,count))
    f.close()
    data.close()