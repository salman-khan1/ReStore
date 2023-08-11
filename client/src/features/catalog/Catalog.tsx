import {Fragment, useEffect, useState}from 'react'
import { Product } from '../../app/models/Product'
import ProductList from './ProductList';
import agent from '../../app/api/agent';
import LoadingComponent from '../../app/layout/LoadingComponent';


const Catalog = () => {
  const [products, setProduct] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true)

useEffect(() => {
    agent.Catalog.list()
    .then(products=>setProduct(products))
    .catch(err=>console.log(err))
    .finally(()=>setLoading(false))
}, []);

if (loading) return <LoadingComponent message='Loading Products'/>

  return (
    <Fragment>
     <ProductList products={products}/>
    </Fragment>
  )
}

export default Catalog