import pandas as pd

def filter_row(row):
    if type(row['word']) is not str:
        return False
    return type(row['word']) is str and row['word'].isalpha()

def filter(df):
    m = df.apply(filter_row, axis=1)
    return df[m]

def filter_raw_table():
    t = pd.read_csv("Dataframe/raw_table.csv")
    t = filter(t)
    t.to_csv("Dataframe/filtered_table.csv", index=False)
