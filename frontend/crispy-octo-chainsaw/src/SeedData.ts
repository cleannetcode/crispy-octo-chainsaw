import { Course } from './Services/CourseService/useCourseService';

const desc1: string =
  'В курсе большинство шагов — это практические задания на создание SQL-запросов. Каждый шаг включает  минимальные теоретические аспекты по базам данных или языку SQL, примеры похожих запросов и пояснение к реализации.Для создания, выполнения и отладки SQL-запросов используется платформа Stepik, на свой компьютер ничего дополнительно устанавливать не надо.Сложность запросов возрастает по мере прохождения курса. Сначала они формулируются для отдельных таблиц, а затем для баз данных, разработанных для предметных областей, таких как "Интернет-магазин", "Тестирование", "Абитуриент". Причем в процессе выполнения шагов курса решаются практические задачи из выбранной предметной области.Каждый учащийся может придумать свои задания на создание SQL-запросов. В курсе есть модуль, в котором размещаются лучшие из них.Данный курс направлен на то, чтобы научить слушателя создавать базы данных и реализовывать запросы к ним на языке SQL  для различных предметных областей.';
const desc2: string =
  'Go — уникальный язык. Простой до примитивности, но с большой и продуманной стандартной библиотекой. Статически типизирован, но отлично подходит для небольших утилит. Достаточно низкоуровневый, чтобы возиться с байтиками, но достаточно высокоуровневый, чтобы заниматься асинхронщиной без головной боли.Этот курс для тех, кто хорошо знает Python, JS или PHP (или любой другой язык) и хочет быстро освоить Go, чтобы начать применять его на работе или в личных проектах. Мы не будем тратить время на объяснения «что такое переменная», «как работает цикл» и решение бессмысленных упражнений типа «что напечатает функция». Вместо этого изучим язык на небольших практических задачках.Поскольку курс рассчитан на опытных программистов, я выбрал формат изложения, который предпочитаю сам: краткий, емкий, с заданиями средней сложности. Возможно, он понравится и вам.';
const desc3: string =
  '"Поколение Python: курс для начинающих" знакомит школьников и всех, кому это интересно, с программированием. Рассказывает об основных типах данных, конструкциях и принципах структурного программирования, используя версию языка Python ветки 3.x.Выбран Python за ясность кода и быстроту реализации на нем программ.Цель курса - формирование базовых понятий структурного программирования.В нем 8 модулей с теоретическими и практическими материалами и заданиями.';
const desc4: string =
  'Привет, данный курс для тебя, если ты: 1. Планируешь сменить профессию и начать свою карьеру в IT! 2. Задумался о том: как стать тестировщиком? 3. Ищешь качественный материал для обучения по профессии QA Engineer. При должном терпении и усилиях, которые вы приложите при обучении незамедлительно дадут вам хорошую теоретическую и практическую базу для прохождения собеседования и будущей работе по профессии.';
const desc5: string =
  'Курс предназначен для тех, кто уже знает основы Android разработки и языка Kotlin. Здесь вы изучите все, что нужно, чтобы быть готовым к устройству на работу на должность Junior Android Developer. Вас ждут такие темы как Dagger, корутины, чистая архитектура, основные компоненты Android и многое другое';

export const fixtureCourses: Course[] = [
  {
    id: 1,
    description: desc1,
    title: 'Интерактивный тренажер по SQL',
    exercises: [
      {
        id: 1,
        title: 'Команды SQL',
        description: 'В этом упражнении вы узнаете об основных SQL командах',
      },
      {
        id: 2,
        title: 'Операции с данным',
        description: 'В этом упражнении вы узнаете об операциях с данными',
      },
      {
        id: 3,
        title: 'Нормализация баз данных',
        description: 'В этом упражнении вы узнаете о нормализации баз данных',
      },
    ],
    bannerName: 'SQL-Training-300x246.png',
    repositoryName: 'SqlCourse',
  },
  {
    id: 2,
    description: desc2,
    title: 'Thank Go! Golang на практике',
    exercises: [],
    bannerName: 'golang.png',
    repositoryName: 'GoCourse',
  },
  {
    id: 3,
    description: desc3,
    title: '"Поколение Python": курс для начинающих',
    exercises: [],
    bannerName: 'python-code-2.jpg',
    repositoryName: 'PythonCourse',
  },
  {
    id: 4,
    description: desc4,
    title: 'Тестирование ПО с Нуля до Специалиста',
    exercises: [],
    bannerName: 'Manual-Testing.jpg',
    repositoryName: 'TestingCourse',
  },
  {
    id: 5,
    description: desc5,
    title: 'Android профессиональный уровень (Kotlin)',
    exercises: [],
    bannerName: 'kotlin1.jpg',
    repositoryName: 'KotlinCourse',
  },
];