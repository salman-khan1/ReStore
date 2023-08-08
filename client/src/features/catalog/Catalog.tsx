import {Fragment, useEffect, useState}from 'react'
import { Product } from '../../app/models/Product'
import ProductList from './ProductList';


const Catalog = () => {
  const [products, setProduct] = useState<Product[]>([]);

useEffect(() => {
  fetch("http://localhost:5000/api/Products")
    .then((res) => res.json())
    .then((data) => setProduct(data));
}, []);
  return (
    <Fragment>
     <ProductList products={products}/>
    </Fragment>
  )
}

export default Catalog