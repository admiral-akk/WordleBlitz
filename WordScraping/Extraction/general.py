from Extraction.simpsons import extract_simpsons_data
from Extraction.tv_scripts import extract_tv_scripts
from Extraction.meaningpedia import extract_meaningpedia
from Extraction.gutenberg import  extract_gutenberg
from Extraction.contemporary_poetry import  extract_poetry
from Extraction.contemporary_fiction import  extract_fiction
from Extraction.en_wiki import extract_en_wiki
from Extraction.invoke import  extract_invoke

def regenerate_data():
    extract_tv_scripts()
    extract_simpsons_data()
    extract_meaningpedia()
    extract_gutenberg()
    extract_fiction()
    extract_poetry()
    extract_en_wiki()
    extract_invoke()
