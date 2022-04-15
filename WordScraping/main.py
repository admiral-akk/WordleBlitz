# This is a sample Python script.

# Press Shift+F10 to execute it or replace it with your code.
# Press Double Shift to search everywhere for classes, files, tool windows, actions, and settings.

from Extraction.general import regenerate_data
from generate_selective_lists import generate_raw_table
from  filter_table import  filter_raw_table
from  wordle_table import  generate_words_table, generate_answers_table
from  data_gen import  build_answer_table, build_word_table,build_answer_list, build_word_list

if __name__ == '__main__':
    regenerate_data()
    generate_raw_table()
    filter_raw_table()
    build_answer_table()
    build_word_table()
    build_answer_list()
    build_word_list()