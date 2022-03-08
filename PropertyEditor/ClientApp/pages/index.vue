<template>
  <div class="block">
    <!--Диалоговое окно для редактирования и добавления категорий-->
    <Dialog :showDialog="showDialog" v-if="showDialog"
            :mode="dialogMode"
            :editableCategory="editableCategory"
            @reload="getCategories"
            @close="showDialog = false"></Dialog>
    <!---->

    <div class="px-4" style="font-weight: 100; padding-top: 10px; font-size: 25px">Property Editor</div>
    <div class="flex justify-start items-start">
      <!--Категории-->
      <div class="block p-4" style="width: 400px">
        <div>
          <div class="flex items-center justify-between bg-indigo-600 text-white px-2 pl-4" style="padding: 6px 12px">
            <label class="font-bold text-sm">Категории</label>
            <button @click="showDialog = true; dialogMode = 'add'"><svg class="h-5 w-5 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">  <line x1="12" y1="5" x2="12" y2="19" />  <line x1="5" y1="12" x2="19" y2="12" /></svg></button>
          </div>
          <div :class="['px-4 py-1 flex justify-between',
           { 'bg-gray-100': index % 2 == 0 },
           { 'bg-indigo-200': index % 2 != 0 } ]"
               style="border-bottom: 1px solid lightgray"
               v-for="(item, index) in categories"
               >
            <div class="flex items-center space-x-2">
              <button v-if="categoryId != item.id" @click="categoryId = item.id" class="bg-indigo-500 hover:bg-indigo-600 rounded"><svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="12" y1="5" x2="12" y2="19" />  <line x1="5" y1="12" x2="19" y2="12" /></svg></button>
              <button v-else class="bg-red-500 hover:bg-red-600 rounded"><svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="5" y1="12" x2="19" y2="12" /></svg></button>
              <div>{{ item.name }}</div>
            </div>
            <div class="flex items-center space-x-2">
              <button @click="() => { showDialog = true; dialogMode = 'edit'; editableCategory = item }"><svg class="h-5 w-5 text-gray-500" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <path d="M4 20h4l10.5 -10.5a1.5 1.5 0 0 0 -4 -4l-10.5 10.5v4" />  <line x1="13.5" y1="6.5" x2="17.5" y2="10.5" /></svg></button>
              <button @click="() => { showDialog = true; dialogMode = 'delete'; editableCategory = item }"><svg class="h-5 w-5 text-red-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">  <path d="M21 4H8l-7 8 7 8h13a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2z" />  <line x1="18" y1="9" x2="12" y2="15" />  <line x1="12" y1="9" x2="18" y2="15" /></svg></button>
            </div>
          </div>
        </div>
      </div>
      <!---->

      <!--Объекты-->
      <div class="block p-4" style="width: 400px" v-if="objects">
        <div class="flex items-center justify-between bg-indigo-600 text-white pl-4" style="padding: 6px 8px; padding-left: 13px">
          <label class="font-bold text-sm">Список - {{ category ? category.name : '' }}</label>
          <button v-if="category" @click="add"><svg class="h-5 w-5 text-white" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">  <line x1="12" y1="5" x2="12" y2="19" />  <line x1="5" y1="12" x2="19" y2="12" /></svg></button>
        </div>
        <div class="w-full shadow">
          <div class="w-full" v-for="(item, index) in objects">
            <div :class="['w-full px-1 pr-2 w-64 py-1 flex justify-between items-center',
           { 'bg-gray-100': index % 2 == 0 },
           { 'bg-indigo-200': index % 2 != 0 } ]"
                 style="border-bottom: 1px solid lightgray">
              <div class="flex items-center space-x-2">
                <button v-if="editableObject != item" @click="edit(item)"><svg class="h-5 w-5 text-gray-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">  <polyline points="9 18 15 12 9 6" /></svg></button>
                <button v-else @click="cancel">
                  <svg class="h-5 w-5 text-gray-500" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <polyline points="6 9 12 15 18 9" /></svg>
                </button>
                <div>{{ item.name }}</div>
              </div>
              <div class="flex items-center space-x-2">
                <button @click="editableObject = null; removableObject = item"><svg class="h-5 w-5 text-red-500" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">  <path d="M21 4H8l-7 8 7 8h13a2 2 0 0 0 2-2V6a2 2 0 0 0-2-2z" />  <line x1="18" y1="9" x2="12" y2="15" />  <line x1="12" y1="9" x2="18" y2="15" /></svg></button>
              </div>
            </div>

            <!--Форма для редактирования объекта-->
            <div class="bg-gray-50 px-4 py-4 w-full" v-if="editableObject == item || removableObject == item">
              <!--Добавление-->
              <div class="space-y-4" v-if="category && editableObject != null">
                <div class="flex justify-between items-center">
                  <div class="font-bold text-sm">Название</div>
                  <input v-model="object.name" class="outline-none border pl-2 w-64" placeholder="Название объекта" />
                </div>
                <div class="font-bold text-sm text-blue-500">Свойства</div>
                <div class="space-y-0">
                  <div v-for="(property, index) in category.integerProperties" class="flex items-center justify-between">
                    <div class="font-bold text-sm">{{ property.name }}</div>
                    <div class="py-1"><input v-model="object.integerValues[index].value" class="outline-none border pl-2 w-32" type="number" /></div>
                  </div>

                  <div v-for="(property, index) in category.stringProperties" class="flex items-center justify-between">
                    <div class="font-bold text-sm">{{ property.name }}</div>
                    <div class="py-1"><input v-model="object.stringValues[index].value" class="outline-none border pl-2 w-32" /></div>
                  </div>
                </div>
              </div>
              <!---->

              <!--Действия для добавления и редактирования-->
              <div class="flex space-x-1 justify-end pt-4" v-if="editableObject != null && removableObject == null">
                <button @click="cancel(item)" class="bg-red-700 text-sm px-2 text-white hover:bg-red-600" style="padding: 2px 8px">Отмена</button>
                <button v-if="state == 'default'" @click="mode == 'edit' ? editObject(item) : addObject()"
                        class="bg-indigo-700 text-sm px-2 text-white hover:bg-indigo-600" style="padding: 2px 8px">
                  Сохранить
                </button>
                <button v-if="state == 'loading'"
                        class="bg-indigo-700 text-sm px-2 text-white hover:bg-indigo-600" style="padding: 2px 8px">
                  Подождите...
                </button>
              </div>
              <!---->
              <!--Действия для удаления-->
              <div class="flex space-x-1 justify-end pt-4" v-else>
                <button @click="cancel(item)" class="bg-red-700 text-sm px-2 text-white hover:bg-red-600" style="padding: 2px 8px">Отмена</button>
                <button v-if="state == 'default'" @click="deleteObject(item)"
                        class="bg-indigo-700 text-sm px-2 text-white hover:bg-indigo-600" style="padding: 2px 8px">
                  Удалить
                </button>
                <button v-if="state == 'loading'"
                        class="bg-indigo-700 text-sm px-2 text-white hover:bg-indigo-600" style="padding: 2px 8px">
                  Подождите...
                </button>
              </div>
              <!---->
            </div>
            <!---->
          </div>

        </div>
        <div class="bg-gray-100 flex justify-center items-center p-4" v-if="!objects.length">Пусто</div>
      </div>
      <!---->
    </div>
  </div>
</template>

<script>
  import Dialog from '../components/dialog.vue'

  export default {
    name: 'IndexPage',
    components: { Dialog },
    data() {
      return {
        showDialog: false, //Переменная для отображения диалогового окна (Добавление/Редактирование)
        dialogMode: 'add', //Режим диалогового окна (Для категорий)
        categoryId: 1, //ID текущей категории (нужен для передачи в запрос)
        category: null, //Текущая категория (Объекты которой, мы рассматриваем)
        editableCategory: null, //Текущая редактируемая или добавляемая категория

        categories: null, //Категории
        objects: null, //Все объекты

        object: { //Объект для добавления или редактирования
          name: null,
          integerValues: [],
          stringValues: []
        },

        mode: 'view', //Режим для редактирования и просмотра (View, Add, Edit)
        state: 'default', //Для обработки загрузки (Loading)
        editableObject: null, //Текущий редактируемый или добавляемый объект
        removableObject: null //Текущий удаляемый объект
      }
    },
    watch: {
      'categoryId'(to, from) {
        this.getObjectsByCategory(to)
      }
    },
    methods: {
      //GET
      getCategories() {
        this.$axios.$get(`/api/categories`)
          .then((response) => {
            this.categories = response
          })
      },

      //Получение объектов определенной категории
      getObjectsByCategory(categoryId) {
        this.$axios.$get(`/api/objects/getObjectsByCategory?categoryId=${categoryId}`)
          .then((response) => {
            this.category = response.category
            this.objects = response.objects

            this.editableObject = null
            this.removableObject = null
            this.mode = 'view'
          })
      },

      //Form
      //Добавление нового объекта в список
      add() {
        if (this.mode != 'add'
          && this.state != 'loading'
          && !this.objects.some(o => o.id == 0)) {
          this.mode = 'add'

          var category = this.category
          this.object = {
            id: 0,
            name: '',
            integerValues: [],
            stringValues: []
          }

          var integerProperties = category.integerProperties
          var stringProperties = category.stringProperties

          this.object.categoryId = category.id

          //Добавляем массив целочисленных свойств
          for (var i = 0; i < integerProperties.length; i++)
            this.object.integerValues.push({ propertyId: integerProperties[i].id, value: null })

          //Добавляем массив строковых свойств
          for (var i = 0; i < stringProperties.length; i++)
            this.object.stringValues.push({ propertyId: stringProperties[i].id, value: null })

          this.objects.unshift(this.object)
          this.editableObject = this.object
        }
      },
      //Измененение объекта из списка
      edit(item) {
        if (this.state != 'loading') {
          this.mode = 'edit'
          this.object = item
          this.removableObject = null
          
          /*Изменяем строковые свойства
          (на случай, если мы отредактировали категорию, и новые параметры нужно применить
          к объекту)*/
          for (var i = 0; i < this.category.integerProperties.length; i++) {
            var property = this.category.integerProperties[i]
            if (item.integerValues[i] == null)
              item.integerValues.push({ id: 0, propertyId: property.id, value: null })
          }

          /*Изменяем строковые свойства
          (на случай, если мы отредактировали категорию, и новые параметры нужно применить
          к объекту)*/
          for (var i = 0; i < this.category.stringProperties.length; i++) {
            var property = this.category.stringProperties[i]
            if (item.stringValues[i] == null)
              item.stringValues.push({ id: 0, propertyId: property.id, value: null })
          }

          this.editableObject = item //Присваиваем редактируемый объект

          //Проверка, добавляли ли мы объект. Если он пустой, то нужно его удалить
          for (var i = 0; i < this.objects.length; i++) {
            if (this.objects[i].id == 0)
              this.objects.splice(this.objects.indexOf(this.objects[i]), 1)
          }
        }
      },

      //Отправка данных на сервер
      //Добавление нового объекта
      addObject() {
        this.state = 'loading'

        var object = this.object
        this.$axios.$post(`/api/objects`, object).then((response) => {
          this.state = 'default'
          this.editableObject = null
          this.getObjectsByCategory(this.categoryId)
        })
      },
      //Изменение объекта
      editObject(object) {
        this.state = 'loading'

        this.$axios.$put(`/api/objects/${object.id}`, object).then((response) => {
          this.state = 'default'
          this.editableObject = null
          this.getObjectsByCategory(this.categoryId)
        })
      },
      //Удаление объекта
      deleteObject(object) {
        this.state = 'loading'

        this.$axios.$delete(`/api/objects/${object.id}`, object).then((response) => {
          this.state = 'default'
          this.removableObject = null
          this.getObjectsByCategory(this.categoryId)
        })
      },

      //Отмена любого действия с объектом
      cancel(item) {
        if (item.id == 0)
          this.objects.splice(this.objects.indexOf(item), 1)

        this.editableObject = null
        this.mode = 'view'
      }
    },
    created() {
      this.getCategories() //Загружаем весь список категорий

      var categoryId = this.categoryId
      this.getObjectsByCategory(categoryId) //Загружаем объекты определенной категории
    }
  }
</script>
