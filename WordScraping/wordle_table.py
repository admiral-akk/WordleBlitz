import pandas as pd

def answers():
    with open("Downloads/answers.txt", mode='r', encoding='utf-8') as f:
        words = pd.DataFrame({'word' : [line.strip("\n") for line in f.readlines()]})
    return words

def words():
    with open("Downloads/words.txt", mode='r', encoding='utf-8') as f:
        words = pd.DataFrame({'word' : [line.strip("\n") for line in f.readlines()]})
    return words

def generate_table(table, name):
    df = pd.read_csv("Dataframe/filtered_table.csv")
    merged= table.merge(df,how='left')
    merged.to_csv("Dataframe/{0}.txt".format(name))

def generate_words_table():
    generate_table(words(), 'words_merged')

def generate_answers_table():
    generate_table(answers(), 'answers_merged')