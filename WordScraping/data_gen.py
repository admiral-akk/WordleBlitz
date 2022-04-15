import pandas as pd
import math
import re

def included_count(row):
    included_count = 0
    index = 0
    for k in row:
        index += 1
        if index % 2 == 1:
            continue
        if not math.isnan(k) :
            included_count += 1
    return included_count

def add_included_count(df):
    df['included_count'] = df.apply(included_count, axis=1)

def filter_min_included(df, count):
    add_included_count(df)
    return df[df['included_count'] > count]

def filter_caps(df):
    r = re.compile(r'.*[A-Z].*')
    m = df.word.apply(lambda x: bool(r.match(x)))
    return df[~m]

accentedCharacters = "àèìòùÀÈÌÒÙáéíóúýÁÉÍÓÚÝâêîôûÂÊÎÔÛãñõÃÑÕäëïöüÿÄËÏÖÜŸçÇßØøÅåÆæœ"

def filter_accents(df):
    r = re.compile(r'.*[A-Z].*')
    m = df.word.apply(lambda x: any([c for c in accentedCharacters if c in x]))
    return df[~m]

def filter_triple_letter(df):
    r = re.compile(r'(.)(\1)\1+')
    m = df.word.apply(lambda x: bool(r.match(x)))
    return df[~m]

# https://www.petercollingridge.co.uk/blog/language/analysing-english/bigrams/
def illegal_bigrams():
    d = [("q",  "a, b, c, d, e, f, g, h, j, k, l, m, n, o, p, q, r, s, t, v, w, x, y, z", "b, f, g, j, k, p, q, t, v, w, y, z"),
         ("j",	"b, c, d, f, g, h, k, l, m, p, q, r, s, t, v, w, x, y, z",	"c, q, v, w, x, z"),
         ("x",	"d, j, k, r, x, z",	"b, c, d, f, g, h, j, k, l, m, p, q, r, s, t, v, w, x, z"),
         ("v",	"b, c, f, g, h, j, l, m, n, p, q, t, w, x, z",	"c, f, g, h, j, p, q, w"),
         ("z",	"f, h, j, k, p, q, v, x",	"d, f, g, j, k, n, q, r, t, x")]
    l= set()
    for v in d:
        l.update([v[0] + x for x in v[1].strip(" ").split(",")])
        l.update([x + v[0] for x in v[2].strip(" ").split(",")])
    return l

bigrams = illegal_bigrams()

def filter_missing_bigrams(df):
    m = df.word.apply(lambda x: any([bi in x for bi in bigrams]))
    return df[~m]


def generate_scrabble():
    s = set()
    with open("Downloads/scrabble_words.txt", mode='r',encoding='utf-8') as f:
        for line in f.readlines():
            word = line.strip("\n")
            if len(word) > 0:
                s.add(word)
    return s

scrabble_word = generate_scrabble()


def filter_scrabble_words(df):
    m = df.word.apply(lambda x: x.upper() not in scrabble_word)
    return df[~m]

def filter_slurs(df):
    # shit I don't want to see.
    slur_list = ["nigger", "spic", "coon", "dago", "jap", "kike",
                "kyke","niglet", "nip","nigga","paki","pikey","raghead",
                "redskin","gook","zipperhead","wetback","wigger","wop", "bitch", "slut"]
    slurs = set(slur_list + [slur + "s" for slur in slur_list])
    m = df.word.apply(lambda x: x.lower() in slurs)
    df = df[~m]
    slur_roots = set(["nigger"])
    m = df.word.apply(lambda x: any([root in x.lower() for root in slur_roots]))
    return df[~m]

def remove_plurals(df):
    word_list = set(df['word'])
    m = df.word.apply(lambda x: x[:-1] in word_list and x[-1] == 's')
    return df[~m]


def filter(df, min_count):
    print("Filter included, min: {0}".format(min_count))
    df = filter_min_included(df, min_count)
    print("Filter caps")
    df = filter_caps(df)
    print("Filter triple letters")
    df = filter_triple_letter(df)
    print("Filter accents")
    df = filter_accents(df)
    print("Filter illegal bigrams")
    df = filter_missing_bigrams(df)
    print("Filter non scrabble words")
    df = filter_scrabble_words(df)
    print("Filter slurs")
    df = filter_slurs(df)
    return df


def build_answer_table():
    t = pd.read_csv("Dataframe/filtered_table.csv")
    t = filter(t, 4)
    print("Removing plurals")
    t = remove_plurals(t)
    t.to_csv("Dataframe/answers_table.csv", index=False)

def build_answer_list():
    t = pd.read_csv("Dataframe/answers_table.csv")
    with open("Output/answers.txt", mode='w', encoding='utf-8') as f:
        f.truncate(0)
        for word in t['word']:
            f.write(word.upper() + "\n")

def build_word_list():
    words = []
    with open("Downloads/scrabble_words.txt", mode='r', encoding='utf-8') as f:
        for line in f.readlines():
            word = line.strip("\n")
            if len(word) > 0:
                words.append(word)

    t = pd.DataFrame({'word': words})
    t = filter_missing_bigrams(t)
    t = filter_slurs(t)

    with open("Output/words.txt", mode='w', encoding='utf-8') as f:
        f.truncate(0)
        for word in t['word']:
            f.write(word.upper() + "\n")

def build_word_table():
    t = pd.read_csv("Dataframe/filtered_table.csv")
    t = filter(t, 2)
    t.to_csv("Dataframe/words_table.csv", index=False)