import pandas as pd
from collections import defaultdict


def def_value():
    return None


def def_value2():
    return None, None


def load_data_set(path):
    with open(path, mode='r', encoding='utf-8') as f:
        for line in f.readlines():
            s = [x.strip("\n") for x in line.split(',')]
            if not any([char.isdigit() for char in s[0]]) or len(s) == 2:
                l = defaultdict(def_value)
            else:
                l = defaultdict(def_value2)
            break
    with open(path, mode='r', encoding='utf-8') as f:
        for line in f.readlines():
            s = [x.strip("\n") for x in line.split(',')]
            if not any([char.isdigit() for char in s[0]]):
                l[s[0]] = 1
            elif len(s) == 2:
                l[s[1]] = int(s[0])
            else:
                l[s[1]] = (int(s[0]), int(s[2]))
    return l


def add_keys(l, dict):
    for k in dict.keys():
        l.append(k)

def word_list(meaning, poetry, fiction, simpsons, en_wiki, invoke, tv_script, gutenberg):
    words = []
    add_keys(words, meaning)
    add_keys(words, poetry)
    add_keys(words, fiction)
    add_keys(words, simpsons)
    add_keys(words, en_wiki)
    add_keys(words, invoke)
    add_keys(words, tv_script)
    add_keys(words, gutenberg)
    return set(words)


def generate_row(column, word, meaning, poetry, fiction, simpsons, en_wiki, invoke, tv_script, gutenberg):
    return pd.DataFrame([[word],
                  [meaning[word]],
                  [poetry[word]],
                  [fiction[word]],
                  [gutenberg[word]],
                  [simpsons[word][0]], [simpsons[word][1]],
                  [en_wiki[word][0]],[en_wiki[word][1]],
                  [invoke[word][0]],[invoke[word][1]],
                  [tv_script[word][0]],[tv_script[word][1]]], columns=column)


def generate_dict_table(dict, name):
    count_name = name + '_count'
    rank_name = name + '_rank'
    df = {'word': [], rank_name: [], count_name: []}
    for k in dict.keys():
        df['word'].append(k)
        v = dict[k]
        if type(v) is tuple:
            df[rank_name].append(v[0])
            df[count_name].append(v[1])
        else:
            df[rank_name].append(1)
            df[count_name].append(None)
    return pd.DataFrame(data=df)


def generate_table(meaning, poetry, fiction, simpsons, en_wiki, invoke, tv_script, gutenberg):
    df = generate_dict_table(meaning, 'meaning')
    df = df.merge(generate_dict_table(poetry, 'poetry'), how='outer')
    df = df.merge(generate_dict_table(fiction, 'fiction'), how='outer')
    df = df.merge(generate_dict_table(simpsons, 'simpsons'), how='outer')
    df = df.merge(generate_dict_table(en_wiki, 'en_wiki'), how='outer')
    df = df.merge(generate_dict_table(invoke, 'invoke'), how='outer')
    df = df.merge(generate_dict_table(tv_script, 'tv_script'), how='outer')
    df = df.merge(generate_dict_table(gutenberg, 'gutenberg'), how='outer')
    df = df.sort_values(by=['word'])
    return df

def generate_raw_table():
    meaning = load_data_set("ScrapedData/meaningpedia.txt")
    poetry = load_data_set("ScrapedData/contemporary_poetry.txt")
    fiction = load_data_set("ScrapedData/contemporary_fiction.txt")
    simpsons = load_data_set("ScrapedData/simpsons.txt")
    en_wiki = load_data_set("ScrapedData/en_wiki.txt")
    invoke = load_data_set("ScrapedData/invokeit.txt")
    scripts = load_data_set("ScrapedData/tv_scripts.txt")
    gutenberg = load_data_set("ScrapedData/gutenberg.txt")
    t = generate_table(meaning, poetry, fiction, simpsons, en_wiki, invoke, scripts, gutenberg)
    t.to_csv("Dataframe/raw_table.csv", index=False)
    print(t[1:100])
    return t
