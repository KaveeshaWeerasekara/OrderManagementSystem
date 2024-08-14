<template>
    <v-container>
      <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="product.productName"
              :rules="[rules.required]"
              label="Product Name"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="product.price"
              :rules="[rules.required, rules.number]"
              label="Price"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-row>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="product.category"
              :rules="[rules.required]"
              label="Category"
              required
            ></v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              v-model="product.storedQuantity"
              :rules="[rules.required, rules.number]"
              label="Stored Quantity"
              required
            ></v-text-field>
          </v-col>
        </v-row>
        <v-btn color="primary" type="submit">Create</v-btn>&nbsp;
        <v-btn color="secondary" @click="clearForm">Clear</v-btn>
      </v-form>
    </v-container>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import axios from 'axios';
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  const valid = ref(false);
  const product = ref({
    productName: '',
    price: 0,
    category: '',
    storedQuantity: 0,
  });
  
  const rules = {
    required: value => !!value || 'Required.',
    number: value => !isNaN(value) || 'Must be a number.',
  };
  
  const submitForm = async () => {
    if (valid.value) {
      try {
        const response = await axios.post('http://localhost:5099/api/Product/createProduct', product.value);
        if (response.status === 200) {
          console.log('Product created successfully');
          router.push('/products');
        } else {
          console.error('Failed to create product');
        }
      } catch (error) {
        console.error('Error creating product:', error);
      }
    }
  };
  
  const clearForm = () => {
    product.value = {
      productName: '',
      price: 0,
      category: '',
      storedQuantity: 0,
    };
  };
  </script>
  
  <style scoped>
  /* Add any scoped styles here if needed */
  </style>
  