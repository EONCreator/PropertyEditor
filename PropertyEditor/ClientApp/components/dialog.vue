<template>
  <div class="flex justify-center items-center" v-if="showDialog">
    <div class="absolute" style="background: gray; left: 0; right: 0; bottom: 0; top: 0; opacity: 0.7"></div>
    <div class="absolute bg-white border" style="top: 50px; width: 530px">
      <div class="shadow py-2 px-8">
        <div v-if="mode != 'delete'" style="font-size: 23px; font-weight: 100">{{ mode == 'edit' ? 'Редактирование' : 'Добавление' }}</div>
        <div v-else style="font-size: 23px; font-weight: 100">Удаление</div>

        <div class="py-8 space-y-3" v-if="mode != 'delete'">
          <div class="flex justify-between items-center space-x-10">
          <label class="font-bold text-sm">Название</label>
          <input v-model="category.name" class="px-2 text-sm py-1 outline-none border w-full" placeholder="Введите название" />
          </div>
          <div class="p-1" style="border-top: 1px solid whitesmoke"></div>
          <div class="flex justify-between items-center">
            <label class="font-bold text-sm text-indigo-500">Свойства</label>

            <button @click="addProperty" class="flex items-center space-x-1 p-1 px-2 bg-indigo-500
                    hover:bg-indigo-600 text-sm">
            <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24"
                 stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round"
                 stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />
              <line x1="12" y1="5" x2="12" y2="19" />  <line x1="5" y1="12" x2="19" y2="12" />
              </svg> <span class="text-white">Добавить</span>
            </button>
          </div>
          <div class="overflow-y-scroll space-y-3 px-2" style="height: 300px">
            <div class="flex justify-between items-center" v-for="(item, index) in properties">
              <input v-model="item.name" class="px-2 text-sm py-1 outline-none border" placeholder="Название свойства" />
              <select @change="setPropertyType(item, $event.target.value)" class="bg-gray-50 border px-2 py-1 text-sm outline-none">
                <option v-for="propertyType in propertyTypes" :value="propertyType.type" :selected="item.type == propertyType.type">{{ propertyType.name }}</option>
              </select>
              <button @click="properties.splice(index, 1)" class="flex items-center space-x-1 p-1 px-2 bg-red-500
                    hover:bg-red-600 text-sm">
                <svg class="h-5 w-5 text-white" width="24" height="24" viewBox="0 0 24 24" stroke-width="2" stroke="currentColor" fill="none" stroke-linecap="round" stroke-linejoin="round">  <path stroke="none" d="M0 0h24v24H0z" />  <line x1="4" y1="7" x2="20" y2="7" />  <line x1="10" y1="11" x2="10" y2="17" />  <line x1="14" y1="11" x2="14" y2="17" />  <path d="M5 7l1 12a2 2 0 0 0 2 2h8a2 2 0 0 0 2 -2l1 -12" />  <path d="M9 7v-3a1 1 0 0 1 1 -1h4a1 1 0 0 1 1 1v3" /></svg>
                <span class="text-white">Удалить</span>
              </button>
            </div>
          </div>
        </div>
        <div v-else class="py-8 space-y-3">
          Вы действительно хотите удалить данный объект?
        </div>

        <div class="flex items-center justify-end space-x-2">
          <button @click="closeDialog" class="bg-red-500 text-white px-4 py-1 shadow hover:bg-red-600">Отмена</button>
          <button v-if="state != 'loading'" @click="callMethod" class="bg-indigo-500 text-white px-4 py-1 shadow hover:bg-indigo-600">OK</button>
          <button v-else class="bg-indigo-500 text-white px-4 py-1 shadow hover:bg-indigo-600">Подождите</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    name: 'Dialog',
    props: ['showDialog', 'mode', 'editableCategory'],
    data() {
      return {
        properties: [], //Свойства
        propertyTypes: [ //Типы свойств
          { name: 'Целое число', type: 'integer' },
          { name: 'Строка', type: 'string' }
        ],

        //Полученная категория
        category: {
          name: null,
          integerProperties: [],
          stringProperties: []
        },

        state: 'view' //Состояние (Редактирование, Загрузка)
      }
    },
    methods: {
      //Forms
      addProperty() {
        var type = this.propertyTypes.length ? this.propertyTypes[0].type : ''
        this.properties.unshift({ name: null, type })
      },
      setPropertyType(property, type) {
        var index = this.properties.indexOf(property)
        this.properties[index].type = type

        this.properties[index].id = 0
      },

      //Общий метод для действий
      callMethod() {
        var mode = this.mode
        if (mode == 'add')
          this.addCategory()
        else if (mode == 'edit')
          this.editCategory()
        else
          this.deleteCategory()
      },

      //CRUD
      addCategory() {
        if (this.state != 'loading') {
          this.state = 'loading'

          var category = this.category

          //Свойства категории
          var properties = this.properties

          var integerProperties = []
          var stringProperties = []

          for (var i = 0; i < properties.length; i++) {
            var property = properties[i]
            switch (property.type) {
              case 'integer':
                integerProperties.push({ name: property.name });
                break;
              case 'string':
                stringProperties.push({ name: property.name });
                break;
            }
          }

          category.integerProperties = integerProperties
          category.stringProperties = stringProperties

          this.$axios.$post(`/api/categories`, category).then((response) => {
            this.state = 'view'
            this.$emit('reload')
            this.$emit('close')
          })
        }
      },
      editCategory() {
        if (this.state != 'loading') {
          this.state = 'view'

          var category = this.category

          //Свойства категории
          var properties = this.properties

          category.integerProperties = []
          category.stringProperties = []
          
          for (var i = 0; i < properties.length; i++) {
            var property = properties[i]
            switch (property.type) {
              case 'integer':
                category.integerProperties.push({ id: property.id, name: property.name });
                break;
              case 'string':
                category.stringProperties.push({ id: property.id, name: property.name });
                break;
            }
          }

          this.editableCategory.name = category.name
          this.editableCategory.integerProperties = category.integerProperties
          this.editableCategory.stringProperties = category.stringProperties

          this.$axios.$put(`/api/categories/${category.id}`, this.editableCategory).then((response) => {
            this.$emit('reload')
            this.$emit('close')
          })
        }
      },
      deleteCategory() {
        var category = this.editableCategory

        this.$axios.$delete(`/api/categories/${category.id}`).then((response) => {
          this.state = 'view'
          this.$emit('reload')
          this.$emit('close')
        })
      },

      closeDialog() {
        this.$emit('close')
      }
    },
    created() {
      if (this.mode == 'edit') {
        this.category = this.editableCategory

        for (var i = 0; i < this.category.integerProperties.length; i++) {
          var property = this.category.integerProperties[i]
          this.properties.push({ id: property.id, name: property.name, type: 'integer' });
        }
        for (var i = 0; i < this.category.stringProperties.length; i++) {
          var property = this.category.stringProperties[i]
          this.properties.push({ id: property.id, name: property.name, type: 'string' });
        }
      }
    }
  }
</script>
