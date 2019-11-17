
import hljs from 'highlight.js';
import 'highlight.js/styles/github.css';

import javascript from 'highlight.js/lib/languages/javascript';
import html from 'highlight.js/lib/languages/htmlbars';
import java from 'highlight.js/lib/languages/java';
import python from 'highlight.js/lib/languages/python';
import go from 'highlight.js/lib/languages/go';
import json from 'highlight.js/lib/languages/json';
import cs from 'highlight.js/lib/languages/cs';
import css from 'highlight.js/lib/languages/css';
import xml from 'highlight.js/lib/languages/cpp';

const languages = ['javascript', 'htmlbars', 'java', 'cs', 'python', 'go', 'json', 'xml', 'css', 'cpp'];

languages.forEach(language => hljs.registerLanguage(language, require(`highlight.js/lib/languages/${language}`)));

export default hljs;